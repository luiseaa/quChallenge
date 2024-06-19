using System;
using System.Collections.Generic;
using System.Linq;
using WordFinder;

namespace WordFinder
{

    public class Program
    {
        public static void Main()
        {

            var matrix = new List<string>
            {
            "abcdc",
            "fgwio",
            "chill",
            "pqnsd",
            "uvdxy"
            };

            var wordstream = new List<string> { "chill", "cold", "wind", "rain" };

            var wordFinder = new WordFinder(matrix);
            var foundWords = wordFinder.Find(wordstream);

            Console.WriteLine("Found words in matrix : \n");
            foreach (var word in foundWords)
            {
                Console.WriteLine(word);
            }

            if (!foundWords.Any())
            {
                Console.WriteLine("0 coincidences");
            }
        }
    }
}
