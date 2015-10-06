using System;
public partial class CommandTest
{
    public static void Test(string[] args)
    {
        string error;
        CommandLineInfo commandLine = new CommandLineInfo();
        if (!CommandLineHandler.TryParse(args, commandLine, out error))
        {
            Console.WriteLine(error);
            DisplayHelp();
        }
        else
        {
            Console.WriteLine("good cmd line");
        }
    }

    private static void DisplayHelp()
    {
        Console.WriteLine("This is Help");
    }
}