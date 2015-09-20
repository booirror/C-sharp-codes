namespace Hangman
{
    using System.Collections.Generic;
    public abstract class ChoiceStrategy
    {
        public abstract string Choice(List<string> allWords);
    }
}