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