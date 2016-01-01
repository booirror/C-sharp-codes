using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageColor
{
    public partial class MidColor : Form
    {
        private PictureBox pic;
        public MidColor()
        {
            InitializeComponent();
            pic = new PictureBox();
            pic.Width = panel1.Width;
            pic.Height = pic.Height;
            pic.SizeMode = PictureBoxSizeMode.AutoSize;
            pic.Location = new Point(0, 0);
            panel1.Controls.Add(pic);

            panel1.BackColor = Color.Black;
            panel2.BackColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "png file(*.png)|*.png";
            var result = openfile.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = openfile.FileName;
                OpenImage(path);
            }
        }

        private void OpenImage(string path)
        {
            if (path == null || path.Length == 0)
            {
                return;
            }
            Bitmap bmp = new Bitmap(path);
            pic.Image = bmp;
            SetMidColor(bmp);
            this.Refresh();
        }

        private void SetMidColor(Bitmap bmp)
        {
            int grid = 100;
            int count = 0;
            int r = 0;
            int g = 0;
            int b = 0;
            for (int y = 0; y < bmp.Height; y += grid)
            {
                int mh = bmp.Height - y;
                for (int x  =0; x < bmp.Width; x += grid)
                {
                    int mw = bmp.Width - x;
                    int mv = Math.Min(mh, mw);
                    int mwidth = Math.Min(mv, grid);
                    Color c = GetGridColor(bmp, x, y, mwidth);
                    r += c.R;
                    g += c.G;
                    b += c.B;
                    count++;
                }
            }
            Color midc = Color.FromArgb(r / count, g / count, b / count);
            panel2.BackColor = midc;
        }

        private Color GetGridColor(Bitmap bmp, int col, int row, int width)
        {
            int count = 0;
            int r = 0;
            int g = 0;
            int b = 0;
            for (int y = 0; y < width; y++)
            {
                int startY = row + y;
                for (int x = 0; x < width; x++)
                {
                    int startX = col + x;
                    var c = bmp.GetPixel(startX, startY);
                    if (c.A != 0)
                    {
                        r += c.R;
                        g += c.G;
                        b += c.B;
                        count++;
                    }
                }
            }
            return Color.FromArgb(r / count, g / count, b / count);
        }
    }
}
