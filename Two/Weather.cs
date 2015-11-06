using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace YahooWeather
{
    public partial class Weather : Form
    {
        public Weather()
        {
            InitializeComponent();
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dict = new Dictionary<string, string>();
            LoadCityData();
        }

        public static DataSet GetYahooWeather(string cityName)
        {
            DataSet dstWeather = new DataSet();
            DataTable dtb = new DataTable("Weather");
            dstWeather.Tables.Add(dtb);
            dstWeather.Tables["Weather"].Columns.Add("Date", typeof(string));
            dstWeather.Tables["Weather"].Columns.Add("Week", typeof(string));
            dstWeather.Tables["Weather"].Columns.Add("Weather", typeof(string));
            dstWeather.Tables["Weather"].Columns.Add("Tlow", typeof(string));
            dstWeather.Tables["Weather"].Columns.Add("Thigh", typeof(string));
            DataRow drowNone = dstWeather.Tables["Weather"].NewRow();
			drowNone["Week"] = "None";
			drowNone["Weather"] = "None";
			drowNone["Tlow"] = "None";
			drowNone["Thigh"] = "None";

            string code = CityCode(cityName);
            if (code == null) {
                dstWeather.Tables["Weather"].Rows.Add(drowNone);
                return dstWeather;
            } 
            string xml = "http://xml.weather.yahoo.com/forecastrss?p=" + code;
            XmlDocument weather = new XmlDocument();
            weather.Load(xml);
            XmlNamespaceManager nsManager = new XmlNamespaceManager(weather.NameTable);
            nsManager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");
            XmlNodeList nodes = weather.SelectNodes("/rss/channel/item/yweather:forecast", nsManager);

            if (nodes.Count > 0)
            {
                foreach (XmlNode node in nodes)
                {
                    DataRow row = dstWeather.Tables["Weather"].NewRow();
                    row["Date"] = ChinaTimeFormat(node.SelectSingleNode
                        ("@date").Value.ToString());
                    row["Week"] = ChinaWeekFormat(node.SelectSingleNode("@day").Value.ToString());
                    row["Weather"] = node.SelectSingleNode("@text").Value;
                    row["Tlow"] = CelsiusUnit(int.Parse(node.SelectSingleNode
                        ("@low").Value)) + "℃";
                    row["Thigh"] = CelsiusUnit(int.Parse(node.SelectSingleNode
                        ("@high").Value)) + "℃";
                    dstWeather.Tables["Weather"].Rows.Add(row);
                }
            }
            else
            {
                dstWeather.Tables["Weather"].Rows.Add(drowNone);
            }
            return dstWeather;
        }

        private static string CelsiusUnit(int f)
        {
            return Math.Round((f - 32) / 1.8, 1).ToString();
        }


        private static string ChinaTimeFormat(string m)
        {
            return Convert.ToDateTime(m).ToString("yyyy年MM月dd日");
        }

        private static string ChinaWeekFormat(string strEweek)
        {
            switch (strEweek)
            {
                case "Mon":
                    return "星期一";
                case "Tue":
                    return "星期二";
                    break;
                case "Wed":
                    return "星期三";
                    break;
                case "Thu":
                    return "星期四";
                    break;
                case "Fri":
                    return "星期五";
                    break;
                case "Sat":
                    return "星期六";
                    break;
                case "Sun":
                    return "星期日";
                    break;
                default:
                    return "错误";
                    break;
            }
        }

        private static string CityCode(string city)
        {
            if (dict.ContainsKey(city))
            {
                return dict[city];
            }
            return null;
        }

        private void LoadCityData()
        {
            StreamReader reader = new StreamReader("WeatherCityCode.dat");
            string line = reader.ReadLine();
            while (line != null)
            {
                Regex regex = new Regex(@"(?<city>.*)(?<code>CHXX\d+)");
                Match m = regex.Match(line);
                if (m.Success) {
                    dict.Add(m.Result("${city}").Trim(), m.Result("${code}").Trim());
                }
                line = reader.ReadLine();
            }
        }

        private static Dictionary<string, string> dict;

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGrid1.DataSource = GetYahooWeather(textBox1.Text.Trim()).Tables[0];
        }
    }
}
