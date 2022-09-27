using System.Reflection.Metadata;
using System.Security.AccessControl;

namespace Hangman_The_latest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> usedLetters = new List<string>();
            string printabledWord = "";
            int retryCount = 0;
            const int maxCount = 6;
            const string wildCard = "-";
            string wordToGuess = GetRandomWord();

            FileHelper.Save();

            //repalace letters with placeholders in printable version
            foreach (var letter in wordToGuess)
            {
                printabledWord += wildCard;
            }

            PrintHelper.PrintWord(printabledWord);

            while (true) /*game loop*/
            {
                string userInput = GetLetterFromUser();

                if (wordToGuess.ToUpper().Contains(userInput.ToUpper()))
                {
                    // convert to char array to use iondex for replacing letters
                    var charArr = printabledWord.ToCharArray();
                    // swap _ to actual letter
                    for (int i = 0; i < wordToGuess.Length; i++)
                    {
                        if (wordToGuess[i].ToString().ToUpper() == userInput.ToUpper())
                        {
                            charArr[i] = userInput.ToUpper()[0];
                        }
                    }
                    // re-assign new value
                    printabledWord = string.Join("", charArr);
                }
                else
                {
                    if (!usedLetters.Contains(userInput.ToUpper()))
                    {
                        usedLetters.Add(userInput.ToUpper());
                    }
                    ++retryCount;
                   
                }

                PrintHelper.PrintWord(printabledWord);
                PrintHelper.PrintRetries(retryCount, usedLetters);

                if (!printabledWord.Contains(wildCard))
                {
                    Console.WriteLine("You won!");
                    return;
                }

                if (retryCount >= maxCount)
                {
                    Console.WriteLine("Game over!");
                    return;
                }

            }
        }

        static string GetLetterFromUser()
        {
            string userInput;
            while (true)  /*get letter from user*/
            {
                Console.WriteLine("Please enter a letter:");
                userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Please enter a letter!");
                    continue;
                }
                if (userInput.Length > 1)
                {
                    Console.WriteLine("Please enter only one letter!");
                    continue;
                }
                return userInput;
            }
        }
     
        static string GetRandomWord()
        {
            List<string> words = FileHelper.Load();
            Random random = new Random();
            int index = random.Next(words.Count);
            string wordToGuess = words[index];
            return wordToGuess;
        }
    }   
    
}