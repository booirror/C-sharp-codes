namespace Hangman
{
    public class TopCommand:ICommand
    {
        public void Execute()
        {
            var console = new ConsoleWrapper();
            ScoreBoard scores = new ScoreBoard(console);
            scores.Source = "../../Resources/topScores.txt";
            scores.Load();
            scores.Print();
        }
    }
}