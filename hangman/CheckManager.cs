﻿namespace Hangman
{
    using System.Collections.Generic;

    public class CheckManager
    {
        public CheckManager(Player player)
        {
            this.Player = player;
            this.CommandManager = new CommandManager();
            this.HasHelpUsed = false;
            this.UsedLetters = new HashSet<char>();
        }
        public ICommand HelpCommand { get; set; }
        public ICommand TopCommand { get; set; }
        public ICommand RestartCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand UsedCommand { get; set; }
        public CommandManager CommandManager { get; set; }
        public Player Player { get; set; }
        public bool HasHelpUsed { get; set; }
        public HashSet<char> UsedLetters { get; set; }

        public void CheckCommand(string playerChoice, IWord word)
        {
            var playerChoiceToLower = playerChoice.ToLower();
            if (playerChoiceToLower == Command.Help.ToString().ToLower())
            {
                if (this.HasHelpUsed == true)
                {
                    UIMassages.UsedHelpOptionMessage();
                    UIMassages.SecretWordMessage(word.PrintView, false);
                }
                else
                {
                    this.CommandManager.Proceed(this.HelpCommand);
                    this.HasHelpUsed = true;
                    if (word.NumOfRevealedLetters < word.WordLength)
                    {
                        UIMassages.SecretWordMessage(word.PrintView, false);
                    }
                }
            }
            else if (playerChoiceToLower == Command.Restart.ToString().ToLower())
            {
                this.CommandManager.Proceed(this.RestartCommand);
            }
            else if (playerChoiceToLower == Command.Exit.ToString().ToLower())
            {
                this.CommandManager.Proceed(this.ExitCommand);
            }
            else if (playerChoiceToLower == Command.Used.ToString().ToLower())
            {
                this.CommandManager.Proceed(this.UsedCommand);
            }
        }

        public void CheckLetterAccordance(IWord word, char playerLetter)
        {
            bool isMatch = false;
            this.AddLetterInUsed(playerLetter);
            char[] wordAsChars = word.PrintView.ToCharArray();
            for (int i = 0; i < word.WordLength; i++)
            {
                if (playerLetter == word.Content[i])
                {
                    if (!word.RevealedCharacters[i])
                    {
                        wordAsChars[i] = word.Content[i];
                        isMatch = true;
                        word.RevealedCharacters[i] = true;
                    }
                }
            }
            word.PrintView = new string(wordAsChars);
            if (isMatch)
            {
                UIMassages.RevealedLetterMessage(word.NumOfRevealedLetters, this.Player.AttemptsToGuess);
            }
            else
            {
                this.Player.AttemptsToGuess++;
                UIMassages.NotGuessedLetterMessage(playerLetter, this.Player.AttemptsToGuess);
            }
        }

        public void DefineCommands(IWord secretWord)
        {
            this.HelpCommand = new HelpCommand(secretWord);
            this.TopCommand = new TopCommand();
            this.RestartCommand = new RestartCommand();
            this.ExitCommand = new ExitCommand();
            this.UsedCommand = new UsedCommand(this.UsedLetters);
        }

        public void AddLetterInUsed(char letter)
        {
            if (!this.UsedLetters.Contains(letter))
            {
                this.UsedLetters.Add(letter);
            }
        }
    }
     
}