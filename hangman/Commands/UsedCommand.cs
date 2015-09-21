namespace Hangman
{
    using System;
    using System.Collections.Generic;

    public class UsedCommand : ICommand
    {
        private const int ALLLEFTERSIZE = 26;
        private const ConsoleColor REDCOLOR = ConsoleColor.Red;
        private const ConsoleColor DEFAULTCOLOR = ConsoleColor.Gray;

        public UsedCommand(HashSet<char> usedLetters)
        {
            this.UsedLetters = usedLetters;
            this.AllLetters = this.AddAllLetters();
        }

        public HashSet<char> UsedLetters { get; set; }
        public List<Letter> AllLetters { get; set; }

        public void Execute()
        {
            this.SetColorToTheUsedLetters();
            this.PrintAllLetters();
        }

        private List<Letter> AddAllLetters()
        {
            var allLetters = new List<Letter>();
            Letter example = new Letter();
            for (int i = 0; i < ALLLEFTERSIZE; i++)
            {
                Letter currLetter = (Letter)example.Clone();
                currLetter.Sign = Convert.ToChar(currLetter.Sign + 1);
                allLetters.Add(currLetter);
            }
            return allLetters;
        }

        private void SetColorToTheUsedLetters()
        {
            for (int i = 0; i < ALLLEFTERSIZE; i++)
            {
                var currLetter = this.AllLetters[i];
                if (this.UsedLetters.Contains(currLetter.Sign) && currLetter.Color != REDCOLOR)
                {
                    this.AllLetters[i].Color = REDCOLOR;
                }
            }
        }

        public void PrintAllLetters()
        {
            for (int i = 0; i < this.AllLetters.Count; i++)
            {
                Console.ForegroundColor = this.AllLetters[i].Color;
                this.AllLetters[i].Print();
            }
            Console.ForegroundColor = DEFAULTCOLOR;
            Console.WriteLine();
        }
    }
}