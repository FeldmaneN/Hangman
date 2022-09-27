using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman_The_latest
{
    internal class FileHelper
    {
        internal const string filePath = "words.txt";
        internal static void Save()
        {
            var words = new List<string>() {"LION", "TIGER", "ELEPHANT", "GIRAFFE"};
            File.WriteAllLines(filePath, words);           
        }

        internal static List<string> Load()
        {
            return File.ReadAllLines(filePath).ToList();

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }
        }
    }
}
