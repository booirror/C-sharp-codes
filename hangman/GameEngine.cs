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
            this.Player = player;
            this.ConsoleWrapper = consoleWrapper;
            this.ChoiceStrategy = new ChoiceRandom();
            this.PathToSecretWordsDirectory = PathToSecretWordsDirectory;
        }

        private Player Player { get; set; }
        private CheckManager CheckManager { get; set; }
        private ScoreBoard ScoreBoard { get; set; }
        public ChoiceStrategy ChoiceStrategy { get; set; }
        public IConsole ConsoleWrapper { get; set; }
        public string PathToSecretWordsDirectory { get; set; }

        public void InitializeData()
        {
            Console.Clear();
            this.CheckManager = new CheckManager(this.Player);
            this.ScoreBoard = new ScoreBoard(this.ConsoleWrapper);
            this.Player.AttemptsToGuess = PlayScore;
            this.CheckManager.HasHelpUsed = false;
            var secretWord = this.LoadSecretWord();
            this.GamePlayStart(secretWord);
        }

        private IWord LoadSecretWord()
        {
            SecretWordManager wordManager = new SecretWordManager();
            wordManager.LoadAllSecretWords(this.PathToSecretWordsDirectory);
            List<string> allWords = wordManager.GetAllSecretWords();
            IWord secretWord = new ProxyWord(this.ChoiceWord(this.ChoiceStrategy, allWords));
            this.CheckManager.DefineCommands(secretWord);
            UIMassages.WelcomeMessage(MaxAttempts);
            return secretWord;
        }

        private string ChoiceWord(ChoiceStrategy choice, List<string> words)
        {
            string chosenSecretWord = choice.Choice(words);
            return chosenSecretWord;
        }

        private void GamePlayStart(IWord word)
        {
            while (word.NumOfRevealedLetters < word.Content.Length && this.Player.AttemptsToGuess < 10)
            {
                UIMassages.SecretWordMessage(word.PrintView, false);
                this.InputData(word);
            }
            this.GameOver(word);
        }

        public void InputData(IWord word)
        {
            while (true)
            {
                UIMassages.InviteForGuessOrCommandMessage();
                string playerChoice = this.ConsoleWrapper.ReadLine().ToLower();
                if (playerChoice == string.Empty)
                {
                    continue;
                }

                char playerLetter = playerChoice.ToLower()[0];
                if (playerChoice.Length > 1)
                {
                    if (this.IsTheCommandCorrect(playerChoice))
                    {
                        this.CheckManager.CheckCommand(playerChoice, word);
                    }
                    else
                    {
                        UIMassages.IncorrectInputMessage();
                    }
                    if (word.NumOfRevealedLetters == word.WordLength)
                    {
                        break;
                    }
                }
                else
                {
                    if (char.IsLetter(playerLetter))
                    {
                        this.CheckManager.CheckLetterAccordance(word, playerLetter);
                    }
                    else
                    {
                        UIMassages.IncorrectInputMessage();
                    }
                    break;
                }
            }
        }

        private void GameOver(IWord word)
        {
            if (this.Player.AttemptsToGuess == MaxAttempts)
            {
                UIMassages.LostGameMessage();
            }
            else
            {
                UIMassages.GuessAllWordMessage(this.Player.AttemptsToGuess);
                UIMassages.SecretWordMessage(word.Content, true);
                this.ScoreBoard.Update(this.Player);
                this.ScoreBoard.Print();
            }
        }

        private bool IsTheCommandCorrect(string command)
        {
            var commandToLower = command.ToLower();
            if (Command.Exit.ToString().ToLower() == commandToLower
            || Command.Help.ToString().ToLower() == commandToLower
            || Command.Restart.ToString().ToLower() == commandToLower
                || Command.Top.ToString().ToLower() == commandToLower
            || Command.Used.ToString().ToLower() == commandToLower)
            {
                return true;
            }
            return false;
        }
    }
}