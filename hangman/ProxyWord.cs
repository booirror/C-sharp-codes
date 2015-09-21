namespace Hangman
{
    using System;
    using System.Linq;
    using System.Text;

    public class ProxyWord: Word
    {
        private const char UnrevealedLetterChar = '-';
        public ProxyWord(string word) :base(word)
        {
            this.RealWord = new RealWord(word);
            this.WordLength = this.Content.Length;
            this.RevealedCharacters = new bool[this.WordLength];
            this.PrintView = new string(UnrevealedLetterChar, this.WordLength);
        }

        public override string PrintView
        {
            get;
            set;
        }

        public IWord RealWord { get; set; }

        public override string Print()
        {
            if (this.RevealedCharacters.Contains(false))
            {
                this.FormPrintView();
                return this.PrintView;
            }
            return this.RealWord.Print();
        }

        private void FormPrintView()
        {
            StringBuilder printView = new StringBuilder();
            for (int currChar = 0; currChar < this.WordLength; currChar++) {
                if (this.RevealedCharacters[currChar]) {
                    printView.Append(this.Content[currChar]);
                } else {
                    printView.Append(UnrevealedLetterChar);
                }
            }
            this.PrintView = printView.ToString();
        }
    }
}