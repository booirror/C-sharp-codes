using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriceFetcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Icon = this.Icon;
            timer1.Interval = 15000;
            timer1.Enabled = true;
            timer1_Tick(null, null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            notifyIcon1.Text = FetchData();
            notifyIcon1.BalloonTipText = notifyIcon1.Text;
            notifyIcon1.ShowBalloonTip(3);
        }

        private static string dataUrl = "http://quote.zhijinwang.com/xml/ag.txt?";

        public static string FetchData()
        {
            string url = dataUrl + ToJsTime(DateTime.Now);
            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                client.Headers.Add("Referer", "http://quote.zhijinwang.com/ag.swf");

                byte[] data = client.DownloadData(url);
                string msg = System.Text.Encoding.Default.GetString(data);
                string tag = "gold=|";
                if (msg.IndexOf(tag) >= 0)
                {
                    msg = msg.Substring(msg.IndexOf(tag) + tag.Length);
                    string[] words = msg.Split('|');
                    return "price=" + words[0];
                }
            }catch(Exception e)
            {

            }
            return ":)";
        }

        private static DateTime time1970 = new DateTime(1970, 1, 1);
        public static long ToJsTime(DateTime now)
        {
            TimeSpan ts = now - time1970;
            return (long)ts.TotalMilliseconds;
        }
    }
}
