namespace Hangman
{
    using System;
    using System.Linq;

    public class Word:IWord
    {
        private string content;
        public Word(String word)
        {
            this.content = word;
        
        }

        public virtual string PrintView { get; set; }

        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("The word can not be null");
                }

                if (!this.IsLetterOnly(value))
                {
                    throw new ArgumentException("The word contains non-alphabetic symbols");
                }
                this.content = value;
            }
        }

        public bool[] RevealedCharacters
        {
            get;
            set;
        }

        public int NumOfRevealedLetters
        {
            get
            {
                return this.RevealedCharacters.Where(x => x).Count();
            }
            set
            {
               
            }
        }

        public int WordLength
        {
            set;
            get;
        }


     

        public virtual string Print()
        {
            return this.content;
        }

        public bool IsLetterOnly(string str)
        {
            foreach (char curr in str)
            {
                if (!char.IsLetter(curr))
                {
                    return false;
                }
            }
            return true;
        }
    }
}