using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeLineCounter
{
    public partial class CounterForm : Form
    {
        public CounterForm()
        {
            InitializeComponent();
            totalFiles = new ArrayList();
        }

        private void CurrDirectory_Click(object sender, EventArgs e)
        {
            string currDirectory = System.Environment.CurrentDirectory;
            ListFiles(currDirectory);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.ShowDialog();
            string path = folder.SelectedPath;
            if (path == null || path == string.Empty)
                return;
            ListFiles(path);
        }

        private void ListFiles(string directory)
        {
            totalFiles.Clear();
            lines.Text = "0";
            string[] exts = new string[] { "*.cs", "*.cpp", "*.c", "*.h", "*.hpp", "*.lua", "*.java" };
            foreach (string ext in exts)
            {
                ArrayList list = GetFilesByExtension(directory, ext);
                foreach (string s in list)
                {
                    totalFiles.Add(s);
                }
            }
            this.checkedListBox.Items.Clear();

            ArrayList tempfiles = new ArrayList();
            foreach(string str in totalFiles)
            {
                string s = str.Substring(str.LastIndexOf('\\'));
                if (s.Length > 29)
                    tempfiles.Add("..."+ s.Substring(s.Length - 25, 25));
                else
                    tempfiles.Add(s.Substring(1));
            }
            foreach (string file in tempfiles)
            {
                this.checkedListBox.Items.Add(file);
            }
        }

        private ArrayList GetFilesByExtension(string directory, string extension)
        {
            ArrayList list = new ArrayList();
            string[] directorys = Directory.GetDirectories(directory);
            foreach (string floder in directorys)
            {
                ArrayList fs = GetFilesByExtension(floder, extension);
                foreach (string f in fs)
                {
                    list.Add(f);
                }
            }
            string[] files = Directory.GetFiles(directory, extension);
            foreach (string f in files)
            {
                FileInfo info = new FileInfo(f);
                if (info.Attributes != FileAttributes.Hidden)
                    list.Add(f);
            }
            return list;
        }

        

        private void count_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> langcout = new Dictionary<string,int>();
            for (int i = 0; i < checkedListBox.Items.Count; i++ )
            {
                if (checkedListBox.GetItemChecked(i))
                {
                    string file = (string)totalFiles[i];
                    int lines = CodeLineCount(file);
                    string ext = file.Substring(file.LastIndexOf('.'));
                    if (!langcout.ContainsKey(ext))
                    {
                        langcout.Add(ext, lines);
                    } else
                    {
                        langcout[ext] = langcout[ext] + lines;
                    }
                }
            }
            string text = null;
            foreach (KeyValuePair<string, int> pair in langcout)
            {
                if (text != null)
                {
                    text = text + "\r\n" + string.Format("{0}: {1}", pair.Key, pair.Value);
                }
                else
                {
                    text = string.Format("{0}: {1}", pair.Key, pair.Value);
                }
            }
            if (text == null || text == string.Empty)
            {
                text = "0";
            }
            this.lines.Text = text;
        }

        private int CodeLineCount(string file)
        {
            int count = 0;
            FileStream fstream = new FileStream(file, FileMode.Open);
            StreamReader reader = new StreamReader(fstream);
            string line = reader.ReadLine();
            while (line != null)
            {
                if (line.Trim() != "")
                {
                    count++;
                }
                line = reader.ReadLine();
            }
            reader.Close();
            return count;
        }

        private ArrayList totalFiles;

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool boxChecked = checkBoxAll.Checked;
            for (int i = 0; i < checkedListBox.Items.Count; i++ )
            {
                checkedListBox.SetItemChecked(i, boxChecked);
            }
        }
    }

    
}
