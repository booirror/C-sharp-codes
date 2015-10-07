using System;
using System.IO;

public class PathTest
{
    static public void Test()
    {
        string temp = Path.GetTempFileName();
        System.Console.WriteLine(Path.GetTempPath());
        System.Console.WriteLine(temp);
    }
}