namespace Hangman
{
    using System;
    using System.Collections.Generic;

    public class RandomUtils
    {
        public string RandomizeWord(List<string> secretWords)
        {
            Random rand = new Random();
            int randNum = rand.Next(0, secretWords.Count - 1);
            return secretWords[randNum];
        }

    }
}