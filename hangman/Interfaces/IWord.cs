namespace Hangman
{
    public interface IWord
    {
        string Content { get; set; }
        bool[] RevealedCharacters { get; set; }
        int NumOfRevealedLetters { get; set; }
        int WordLength { get; set; }
        string PrintView { get; set; }
        string Print();
    }
}