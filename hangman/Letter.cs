namespace Hangman
{
    using System;
    public class Letter:ICloneable
    {
        private const ConsoleColor DEFColor = ConsoleColor.Gray;
        private const char DEFSIGN = 'a';

        public Letter()
        {
            this.Sign = DEFSIGN;
            this.Color = DEFColor;
        }

        public char Sign { get; set; }
        public ConsoleColor Color { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Print()
        {
            Console.Write("{0} ", this.Sign);
        }
    }
}