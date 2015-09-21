namespace Hangman
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class SecretWordManager : IExpandable, IRemovable
    {
        private List<string> allSecretWords = new List<string>();


        public void LoadAllSecretWords(string path)
        {
            try
            {
                string[] words = File.ReadAllLines(path);
                foreach (string line in words)
                {
                    this.allSecretWords.AddRange(line.Split(new char[] { ',', ' ', ';', '.' }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("The word library was not found");
            }
            catch (FileLoadException)
            {
                throw new FileLoadException("Unable to load word library");
            }
            catch (PathTooLongException)
            {
                throw new PathTooLongException("The path specified is too long");
            }
            catch (Exception e)
            {
                throw new Exception("All Error occured in: " + e.StackTrace)
            }
        }

        public List<string> GetAllSecretWords()
        {
            return this.allSecretWords;
        }

        public void Add(string newSecretWord)
        {
            this.allSecretWords.Add(newSecretWord);
        }

        public void Remove(int index)
        {
            this.allSecretWords.RemoveAt(index);
        }
    }
}