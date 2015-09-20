namespace Hangman
{
    using System;
    public class ConsoleWrapper : IConsole
    {

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}