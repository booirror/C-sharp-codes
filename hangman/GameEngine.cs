namespace Hangman
{
    using System;
    using System.Collections.Generic;

    public class GameEngine
    {
        private const int PlayScore = 0;
        private const int MaxAttempts = 10;
        private const string PathToSecretWord = @"../../Resource/secretWordsLibrary.txt";

        public GameEngine(Player player, IConsole consoleWrapper)
        {
            this.play
        }

        private Player Player { get; set; }
        private 
        public ChoiceStrategy ChoiceStrategy { get; set; }
        public IConsole ConsoleWrapper { get; set; }
        public string PathToSecretWordsDirectory { get; set; }
    }
}