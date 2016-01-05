using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TextureUnpack
{
    
    public partial class TextureUnpack : Form
    {
        public TextureUnpack()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            this.textBox1.Text = file.FileName;

        }

        private void unpack_Click(object sender, EventArgs e)
        {
            Dictionary<string, FrameInfo> frames = new Dictionary<string, FrameInfo>();
            MetaData metadata = null;
            //Dictionary<string, Dictionary<string, FrameInfo>> plist = new Dictionary<string, Dictionary<string, FrameInfo>>();
            string path = this.textBox1.Text;
            if (path.Length == 0) return;
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            string xpath = "/plist/dict";
            XmlNode node = xml.SelectSingleNode(xpath);

            XmlNode xmlnode = node.FirstChild;
            if (xmlnode != null)
            {
                string key = xmlnode.InnerText;
                XmlNode valNode = xmlnode.NextSibling;
                XmlNode fkey = valNode.FirstChild;
                while (fkey != null)
                {
                    XmlNode fval = fkey.NextSibling;
                    var frame = fval.FirstChild;
                    var frameval = frame.NextSibling;
                    var offset = frameval.NextSibling;
                    var offsetval = offset.NextSibling;
                    var rotated = offsetval.NextSibling;
                    var rotatedval = rotated.NextSibling;

                    var color = rotatedval.NextSibling;
                    var colorval = color.NextSibling;

                    var sourceSize = colorval.NextSibling;
                    var sourceSizeval = sourceSize.NextSibling;
                    var framedata = new FrameInfo(frameval.InnerText, offsetval.InnerText,
                        rotatedval.Name, colorval.InnerText, sourceSizeval.InnerText);
                    
                    frames.Add(fkey.InnerText, framedata);
                    fkey = fval.NextSibling;
                }
                var meta = valNode.NextSibling;
                var metaval = meta.NextSibling;
                if (metaval != null)
                {
                    var fmt = metaval.FirstChild;
                    var fmtval = fmt.NextSibling;

                    var real = fmtval.NextSibling;
                    var realval = real.NextSibling;

                    var sz = realval.NextSibling;
                    var szval = sz.NextSibling;

                    var update = szval.NextSibling;
                    var updateval = update.NextSibling;

                    var name = updateval.NextSibling;
                    var nameval = name.NextSibling;

                    metadata = new MetaData(fmtval.InnerText, realval.InnerText, szval.InnerText);
                }
            }
            string mainpath = path.Substring(0, path.LastIndexOf("\\") + 1);
            string pngfile = mainpath + metadata.realTexName;
            Image image = Image.FromFile(pngfile);
            foreach (var pair in frames)
            {
                string name = pair.Key;
                FrameInfo info = pair.Value;
                Bitmap img = null;
                if (info.Rotated)
                {
                    img = new Bitmap(info.SourceSize.Height, info.SourceSize.Width);
                } else
                {
                    img = new Bitmap(info.SourceSize.Width, info.SourceSize.Height);
                }
                Graphics g = Graphics.FromImage(img);
                if (info.Rotated)
                {
                    Rectangle destRect = new Rectangle(info.srcRect.Location.X, info.srcRect.Location.Y, info.srcRect.Height, info.srcRect.Width);
                    Rectangle srcRect = new Rectangle(info.Frame.Location, info.Frame.Size);
                    srcRect.Width = info.Frame.Size.Height;
                    srcRect.Height = info.Frame.Size.Width;

                    g.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                else
                {
                    g.DrawImage(image, info.srcRect, info.Frame, GraphicsUnit.Pixel);
                }
                
                g.Dispose();

                img.Save(mainpath + name, ImageFormat.Png);
            }
            MessageBox.Show("ok");
        }
    }

    public class MetaData
    {
        public int Format { set; get; }
        public string realTexName { set; get; }
        public Size Size { set; get; }

        public MetaData(string fmt, string name, string sz)
        {
            Regex freg = new Regex(@"(?<integer>\d+?)");
            Format = int.Parse(fmt);
            realTexName = name;
            MatchCollection mc = freg.Matches(sz);
            if (mc.Count == 2)
            {
                int w = int.Parse(mc[0].Result("${integer}"));
                int h = int.Parse(mc[1].Result("${integer}"));
                this.Size = new Size(w, h);
            }
        }
    }

    public class FrameInfo
    {
        public Rectangle Frame { set; get; }
        public Point Offset { set; get; }
        public bool Rotated { set; get; }
        public Rectangle srcRect { set; get; }
        public Size SourceSize { set; get; }

        public FrameInfo(string frame, string offset, string rotated, string srcColor, string size)
        {
            Regex freg = new Regex(@"(?<integer>\d+)");
            MatchCollection mc = freg.Matches(frame);
            if (mc.Count == 4)
            {
                int x = int.Parse(mc[0].Result("${integer}"));
                int y = int.Parse(mc[1].Result("${integer}"));
                int w = int.Parse(mc[2].Result("${integer}"));
                int h = int.Parse(mc[3].Result("${integer}"));
                this.Frame = new Rectangle(x, y, w, h);
            }
            mc = freg.Matches(srcColor);
            if (mc.Count == 4)
            {
                int x = int.Parse(mc[0].Result("${integer}"));
                int y = int.Parse(mc[1].Result("${integer}"));
                int w = int.Parse(mc[2].Result("${integer}"));
                int h = int.Parse(mc[3].Result("${integer}"));
                this.srcRect = new Rectangle(x, y, w, h);
            }
            mc = freg.Matches(offset);
            if (mc.Count == 2)
            {
                int x = int.Parse(mc[0].Result("${integer}"));
                int y = int.Parse(mc[1].Result("${integer}"));
                this.Offset = new Point(x, y);
            }
            this.Rotated = bool.Parse(rotated);
            mc = freg.Matches(size);
            if (mc.Count == 2)
            {
                int w = int.Parse(mc[0].Result("${integer}"));
                int h = int.Parse(mc[1].Result("${integer}"));
                this.SourceSize = new Size(w, h);
            }
        }
    }

}
