using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTest
{
    class Statistics
    {
        public ArrayList LoadData()
        {
            ArrayList list = new ArrayList(30000);
            FileStream fs = new FileStream("Ci.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs);
            string line = reader.ReadLine();
            while (line != null)
            {
                line = line.Trim();
                if (line.Length > 22)
                {
                    list.Add(line);
                }
                line = reader.ReadLine();
            }

            reader.Close();
            fs.Close();
            return list;
        }

        public void SaveData(Dictionary<string, int> dict, int len)
        {
            ArrayList list = new ArrayList();
            FileStream fs = new FileStream(string.Format("result_{0}word.txt", len), FileMode.OpenOrCreate, FileAccess.Write);
            fs.SetLength(0);
            StreamWriter write = new StreamWriter(fs);

            List<KeyValuePair<string ,int>> data = dict.OrderByDescending(x => x.Value).ToList();
            foreach (KeyValuePair<string, int> kv in data)
            {
                write.WriteLine(kv.Key + " " + kv.Value);
            }
            write.Close();
            fs.Close();
        }

        public static readonly char[] WhiteSpaceChars = new char[] {
            '\n', '\t', '\r', ' ', ',', '，', '。', '、', (char)0x0, '□', 'w', '.', 'X', 'T'
        };
        public void StatWord(int len)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            ArrayList list = LoadData();
            Console.ForegroundColor = ConsoleColor.Green;

            int cnt = list.Count;
            int idx = 0;
            foreach (string text in list)
            {
                idx++;
                if (idx % 30 == 0)
                {
                    ShowInfo(idx, cnt);
                }
                for (int i = 0; i < text.Length - 1; i++)
                {
                    string word = text.Substring(i, len);
                    word = word.Trim(WhiteSpaceChars);
                    if (word.Length < len)
                        continue;
                    if (dict.ContainsKey(word))
                    {
                        dict[word]++;
                    }
                    else
                    {
                        dict.Add(word, 1);
                    }
                }
            }
            SaveData(dict, len);
        }

        public void ShowInfo(int i, int cnt)
        {
            Console.Clear();
            Console.WriteLine("{0}/{1}    {2:F2}%", i, cnt, i*100.0/cnt);
        }
    }
}
