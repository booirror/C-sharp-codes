using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReaderResX
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = this.textBox1.Text;
            //this.readResX(path);
            int lines = this.LineCount(path);
            this.label1.Text = "" + lines;
        }

        public void readResX(string path)
        {
            ResourceReader rd = new ResourceReader(path);
            foreach (DictionaryEntry d in rd)
            {
                Image image = (Image)d.Value;
                image.Save("" + d.Key + ".jpg");
            }
        }

        public int LineCount(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader read = new StreamReader(fs);
            int cnt = 0;
            string line = read.ReadLine();
            while (line != null)
            {
                if (line.Trim() != "")
                {
                    cnt++;
                }
                line = read.ReadLine();
            }
            read.Close();
            return cnt;
        }
    }
}
