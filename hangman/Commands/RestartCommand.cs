namespace Hangman
{
    public class RestartCommand : ICommand
    {
        public void Execute()
        {
            HangmanMain.Main();
        }
    }
}