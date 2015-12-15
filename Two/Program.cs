using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace SimpleTest
{
    class SortLen : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            return ((string)a).Length - ((string)b).Length;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string serial = DESCrypto.GetBoardManufacturer();
            byte[] key = Encoding.ASCII.GetBytes(serial.Substring(0, 8));
            byte[] IV = {0x1, 0x33, 0x44, 0x33,  0x99, 0xEF, 0xAB, 0x90};
            DESCrypto.EncryptData("raw.data", "encrypt.data", key, IV);
            DESCrypto.DecryptData("encrypt.data", "decrypt.data", key, IV);
             */
            /*
            string curr = Process.GetCurrentProcess().ProcessName;

            bool s = false;
            //Console.WriteLine(s.ToString());

            RegexTest.Test();
            */
            //dealData();
            /*
            ArrayList list = new ArrayList() { "abb", "ccd" };
            Console.WriteLine(list.Count);
            Console.WriteLine(list[1] as string);
             * */
            /*
            string s = "我是你";
            foreach(char c in s)
            {
                Console.WriteLine(c);
            }
             */
            /*
            Statistics stat = new Statistics();
            stat.StatWord(1);
            Console.WriteLine("done!");
             */
            /*
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("hello", null);
            foreach(var item in dict)
            {
                ConsoleTest.WriteLine(item.Key);
                ConsoleTest.WriteLine(item.Value);
            }
             */
            //ConsoleTest.Test();
            //RegexTest.Test();

            /*
            string newUrl = "http://bbs.tianya.cn/post-funinfo-3857991-1.shtml";
            Regex regex = new Regex("(?<root>https?://.*?)/");
            Match match = regex.Match(newUrl);
            Console.WriteLine(match.Result("${root}"));
             */
            //RegexTest.Test();

            // bool b = bool.Parse("tRuE");
            //Console.WriteLine(b);

            //VirtualTest.Test();
            int[] a = new int[] { 1, 2, 3 };

            mdf(a);

            Console.WriteLine(a[1]);
        }

        static public void mdf(int[] a)
        {
            a[1] = 5;
        }

        public static void writeOut()
        {

            //确保文件编码是unicode
            FileStream ins = new FileStream(@"D:\programming_langage_csharp\workspace\DaShiCSharp\SongCi\bin\Debug\one.dat", FileMode.Open, FileAccess.Read);
            StreamReader ir = new StreamReader(ins);
            string word = ir.ReadLine();
            while (word != null)
            {
                if (word.Length > 0)
                {
                    Console.WriteLine(word);
                    word = ir.ReadLine();
                }
            }
            ir.Close();
            ins.Close();
        }

        public static string read(string title, string key, string def)
        {
            string path = @"D:\programming_langage_csharp\workspace\DaShiCSharp\pzi.ini";
            IniData ini = new IniData(path);
            return ini.Read(title, key, def);
        }

        public static string readXml()
        {
            string path = @"D:\programming_langage_csharp\workspace\DaShiCSharp\pzi.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode nodes = doc.SelectSingleNode("/data/jp");
            return nodes.InnerText;
        }

        public static void dealData()
        {
            string path = @"D:\programming_langage_csharp\workspace\DaShiCSharp\data.txt";
            string pathA = @"D:\programming_langage_csharp\workspace\DaShiCSharp\data_new.txt";
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(stream);
            string data = read.ReadToEnd();
            read.Close();
            stream.Close();
            FileStream fs = new FileStream(pathA, FileMode.OpenOrCreate, FileAccess.Write);
            fs.SetLength(0);
            StreamWriter write = new StreamWriter(fs);
            Regex regex = new Regex(@"""(?<word>.*?)"",\d*");
            MatchCollection m =  regex.Matches(data);
            ArrayList list = new ArrayList();
            if (m.Count > 0)
            {
                foreach (Match match in m)
                {
                    list.Add(match.Result("${word}"));
                }
            }
            list.Sort(new SortLen());

            foreach(string s in list)
            {
                write.WriteLine(s);
            }
            write.Close();
            fs.Close();
        }

    }
}
