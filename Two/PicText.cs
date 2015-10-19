using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicText
{
    public partial class PicText : Form
    {
        public PicText()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.label1.Text = this.textBox1.Text;
            

        }
        private Image image = null;
        private void PicText_Load(object sender, EventArgs e)
        {
            this.image = this.pictureBox1.Image;
        }
    }
}
