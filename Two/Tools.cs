using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoordinateMaster
{
    public partial class Tools : Form
    {
        public Tools()
        {
            InitializeComponent();
        }
        float mScale = 1;
        string file = null;
        int picwidth = 718;
        int picheight = 600;
        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            file = ofd.FileName;

            updateImage();
        }

        private void updateImage()
        {
            if (file == null) return;
            this.pictureBox1.Width = picwidth;
            this.pictureBox1.Height = picheight;
            bool isHeight = true;
            if (this.radioButton1.Checked)
            {
                isHeight = true;
            }
            else
            {
                isHeight = false;
            }
            Image image = Bitmap.FromFile(file);
            float scale = 0;
            if (isHeight)
            {
                scale = picheight / (float)image.Height;
            }
            else
            {
                scale = picwidth / (float)image.Width;
            }
            this.mScale = scale;
            if (scale >= 1)
            {
                scale = 1;
            }

            float newwidth = (image.Width * scale);
            float newheight = image.Height * scale;
            this.pictureBox1.Width = (int)newwidth;
            Bitmap bmp = new Bitmap((int)newwidth, (int)newheight);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(Brushes.Black, 0, 0, (int)newwidth, (int)newheight);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawImage(image, new Rectangle(0, 0, (int)newwidth, (int)newheight), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            this.pictureBox1.Image = bmp;
            g.Dispose();
            this.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            int realX = (int)(p.X / mScale);
            int realY = (int)(p.Y / mScale);
            string s = string.Format("{0}, {1}", realX, realY);
            this.label1.Text = string.Format("({0})", s);
            Clipboard.SetDataObject(s);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            updateImage();
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            updateImage();
        }
    }
}
