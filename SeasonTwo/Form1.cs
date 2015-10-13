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

namespace GDIPlus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Color.Red, 2);
            Brush brush = new SolidBrush(Color.Blue);

            Font font = new Font("宋体", 24);
            Rectangle rect = new Rectangle(20, 120, 100, 160);
            g.DrawLine(pen, 20, 100, 100, 100);
            g.DrawRectangle(pen, rect);

            g.DrawString("GDI+", font, brush, 20, 20);
            brush.Dispose();
            font.Dispose();
            pen.Dispose();
            g.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            GraphicsPath path = new GraphicsPath(FillMode.Winding);
            path.AddString("你好世界", new FontFamily("华文琥珀"), (int)FontStyle.Regular,
                80, new PointF(10, 20), new StringFormat());
            Brush brush = new LinearGradientBrush(new PointF(0, 0), new PointF(Width, Height), Color.Red, Color.Yellow);
            g.DrawPath(Pens.Black, path);
            g.FillPath(brush, path);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            FontFamily[] famlies = FontFamily.GetFamilies(g);
            Font font;
            string familyString;
            float spacing = 0;
            foreach (FontFamily family in famlies)
            {
                try
                {
                    font = new Font(family, 16, FontStyle.Bold);
                    familyString = "This is The " + family.Name + " family.";
                    g.DrawString(familyString, font, Brushes.Black, new PointF(0, spacing));
                    spacing += font.Height + 5;
                }
                catch { }
            }
        }
    }
}
