using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtractPixel
{
    public partial class ColorExtract : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体为活动窗体
        [DllImport("user32.dll")]//取设备场景
        private static extern IntPtr GetDC(IntPtr hwnd);//返回设备场景句柄
        [DllImport("gdi32.dll")]//取指定点颜色    
        private static extern int GetPixel(IntPtr hdc, Point p);

        private HookGlobal.KeyBordHook keyHook = null;
        public ColorExtract()
        {
            InitializeComponent();
            //SetForegroundWindow(this.Handle);
            this.TopMost = true;
            keyHook = new HookGlobal.KeyBordHook();
            keyHook.OnKeyDownEvent += Form1_KeyDownGlobal;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void Form1_KeyDownGlobal(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.G && e.Control && e.Shift)
            {
                Point p = new Point(MousePosition.X, MousePosition.Y);
                IntPtr hdc = GetDC(IntPtr.Zero);
                int c = GetPixel(hdc, p);
                int r = (c & 0xFF);//转换R
                int g = (c & 0xFF00) >> 8;//转换G
                int b = (c & 0xFF0000) >> 16;//转换B
                this.pictureBox1.BackColor = Color.FromArgb(r, g, b);

                this.textBox1.Text = string.Format("{0}, {1}", p.X, p.Y);
                this.textBox2.Text = string.Format("{0}, {1}, {2}", r, g, b);
                this.textBox3.Text = string.Format("0x{0:X}{1:X}{2:X}", r, g, b);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("hh");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.checkBox1.Checked)
            {
                this.TopMost = false;
                keyHook.Stop();
            }
            else
            {
                this.TopMost = true;
                keyHook.Start();
            }
        }
    }
}
