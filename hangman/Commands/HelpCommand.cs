namespace Hangman
{
    using System.Text;
    public class HelpCommand:ICommand
    {
        public HelpCommand(IWord word)
        {
            this.Word = word;
        }

        public IWord Word { get; set; }

        public void Execute()
        {
            string newWord = this.Word.PrintView;
            for (int idx = 0; idx < newWord.Length; idx++)
            {
                if (!char.IsLetter(newWord[idx]))
                {
                    UIMassages.RevealingNextLetterMessage(this.Word.Content[idx]);
                    newWord = ReplaceLetter(newWord, this.Word.Content[idx], idx);
                    this.Word.RevealedCharacters[idx] = true;
                    break;
                }
            }
            this.Word.PrintView = newWord;
        }

        private static string ReplaceLetter(string dashword, char letter, int position)
        {
            char[] newWord = dashword.ToCharArray();
            newWord[position] = letter;
            return new string(newWord);
        }
    }
}