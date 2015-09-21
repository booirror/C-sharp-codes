namespace Hangman
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class ScoreBoard
    {
        private const string TopScorePath = @"../../Resources/topScores.txt";
        private const int NumberOfTopScores = 5;
        private Dictionary<string, int> scoreBoard = new Dictionary<string, int>();

        public ScoreBoard(IConsole consoleWrap)
        {
            this.Source = TopScorePath;
            this.ConsoleWrapper = consoleWrap;
        }

        public Dictionary<string, int> TopScores
        {
            get
            {
                return this.scoreBoard;
            }

            private set
            {
                this.scoreBoard = value;
            }
        }

        public string Source { get; set; }

        public IConsole ConsoleWrapper { get; set; }

        public void Load()
        {
            string[] scoreTemp;
            try
            {
                string[] scores = File.ReadAllLines(this.Source);
                foreach (string score in scores)
                {
                    scoreTemp = score.Split(',');
                    this.scoreBoard.Add(scoreTemp[0], int.Parse(scoreTemp[1]));
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("The ScoreBoard File was not found");
            }
            catch (FileLoadException)
            {
                throw new FileLoadException("Unable to load scoreboard");
            }
            catch (PathTooLongException)
            {
                throw new PathTooLongException("The path specified is too long!");
            }
        }

        public void AddScore(Player player)
        {
            this.TopScores.Add(player.Name, player.AttemptsToGuess);
            this.ExtractSpecificTopScores();
        }

        public void Save()
        {
            try
            {
                using (StreamWriter scoreWriter = new StreamWriter(this.Source))
                {
                    foreach (var score in this.scoreBoard)
                    {
                        scoreWriter.WriteLine("{0},{1}", score.Key, score.Value);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Unable to save file");
            }
            catch (DirectoryNotFoundException)
            {
                throw new FileNotFoundException("unable to find save directory");
            }
        }

        public void Print()
        {
            int possition = 1;
            Console.WriteLine("{0}****Top {1} Scores****", new string(' ', 5), NumberOfTopScores);
            foreach (var score in this.TopScores)
            {
                Console.WriteLine("{0}. {1} --> {2} mistakes", possition, score.Key, score.Value.ToString());
                possition++;
            }
        }

        public void Update(Player player)
        {
            this.Load();
            if (this.TopScores.Count < NumberOfTopScores || player.AttemptsToGuess < this.TopScores.Values.Last())
            {
                while (true)
                {
                    UIMassages.EnterPlayerNameMessage();
                    player.Name = this.ConsoleWrapper.ReadLine();
                    if (player.Name == string.Empty)
                    {
                        throw new ArgumentException(" the player's name cann't be an empty string");
                    }
                    else if (this.TopScores.ContainsKey(player.Name))
                    {
                        throw new ArgumentException("Existing name!");
                    }
                    break;
                }
                this.AddScore(player);
                this.Save();
            }
        }

        private void ExtractSpecificTopScores()
        {
            this.OrderScore();
            if (this.TopScores.Count > NumberOfTopScores)
            {
                for (int i = NumberOfTopScores; i < this.scoreBoard.Count; i++)
                {
                    this.TopScores.Remove(this.TopScores.ElementAt(i).Key);
                }
            }
        }

        private void OrderScore()
        {
            this.TopScores = this.TopScores.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}