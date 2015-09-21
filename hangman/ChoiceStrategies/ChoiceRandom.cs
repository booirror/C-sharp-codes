namespace Hangman
{
    using System.Collections.Generic;

    public class ChoiceRandom: ChoiceStrategy
    {
        public override string Choice(List<string> allWords)
        {
            RandomUtils rand = new RandomUtils();
            return rand.RandomizeWord(allWords);
        }
    }
}