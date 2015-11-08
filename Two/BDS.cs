using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiduAutoComplete
{
    public partial class BDS : Form
    {
        public BDS()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private Random rand = new Random();
        private string prefix = "";

        private string GetBaiduSuggestion(string wd)
        {
            string url = "http://suggestion.baidu.com/su?wd=" + UrlEncode(wd);
            url += "&rnd=" + rand.Next();
            string sug = DownloadString(url);
            sug = Regex.Replace(sug, @".*,s:\[(.*?)\].*", "$1");
            return sug;
        }

        private string DownloadString(string url)
        {
            WebClient client = new WebClient();
            client.Credentials = new System.Net.CredentialCache();
            client.Headers["Cookie"] = "BDUSS=FkcmZZckFNN1h3V0JxdDN4aWFVWmI0bDVwakpzYn5BZn5ZQ25KQkxOVGtvQlpOQVFBQUFBJCQAAAAAAAAAAApRLgtnzNkJZHN0YW5nMjAwMAAAAAAAAAAAAAAAAAAAAAAAAAAAAADAymRxAAAAAMDKZHEAAAAAuFNCAAAAAAAxMC4yMy4yNOQT70zkE";
            byte[] data = client.DownloadData(url);
            return Encoding.Default.GetString(data);
        }

        private string UrlEncode(string wd)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(wd);
            string res = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                res += "%" + bytes[i].ToString("X2");
            }
            return res;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = this.textBox1.Text;
            showSuggest(text);
        }

        private void showSuggest(string text)
        {
            string[] words = text.Split("', \"".ToCharArray());
            string wd = words[words.Length - 1];
            prefix = text.Substring(0, text.Length - wd.Length);
            string sug = GetBaiduSuggestion(wd);
            if (sug == null || sug == "")
                return;
            this.listBox1.Items.Clear();
            string[] ary = sug.Split(',');
            foreach (string s in ary)
            {
                this.listBox1.Items.Add(prefix + s.Replace("'", "").Replace("\"", ""));
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex < 0) return;
            string text = this.listBox1.SelectedItem.ToString();
            this.textBox1.Text = text;
            this.textBox1.SelectionStart = text.Length;
            showSuggest(text);
        }
    }
}
