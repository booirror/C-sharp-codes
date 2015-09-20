namespace Hangman
{
    public class CommandManager
    {
        public void Proceed(ICommand cmd)
        {
            cmd.Execute();
        }
    }
}