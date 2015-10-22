using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class Person
{
    private string name;
    private bool sex;

    public Person(string name, bool sex)
    {
        this.name = name;
        this.sex = sex;
    }

    public override string ToString()
    {
        return "姓名:" + this.name + "\t性别:" + (this.sex ? "男" : "女");
    }
}

[Serializable]
public class Programmer : Person
{
    private string language;

    public Programmer(string name, bool sex, string language):base(name, sex)
    {
        this.language = language;
    }

    public override string ToString()
    {
        return base.ToString() + "\t编程语言:" + this.language;
    }
}

class SerializableTestCS
{
    public static void Test()
    {
        List<Programmer> list = new List<Programmer>();
        list.Add(new Programmer("张三", true, "C#"));
        list.Add(new Programmer("李四", true, "C++"));
        list.Add(new Programmer("王五", true, "Java"));

        string fileName = "serializable.dat";
        Stream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);

        BinaryFormatter binFormat = new BinaryFormatter();
        binFormat.Serialize(fs, list);

        list.Clear();
        
        fs.Position = 0;
        list = (List<Programmer>)binFormat.Deserialize(fs);
        fs.Close();
        foreach (Programmer p in list)
        {
            Console.WriteLine(p);
        }
        Console.Read();
    }
}