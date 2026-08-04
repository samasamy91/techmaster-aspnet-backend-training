using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill06_WordCounter
    {
        public static void Run()
        {
            Console.Write("Enter sentence : ");
            string sentence=Console.ReadLine();
            if (string.IsNullOrWhiteSpace(sentence))
            {
                Console.WriteLine("Sentence cannot be empty");
            }
            else
            {
                sentence = sentence.Trim();
                string[] words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine("Word count: " + words.Length);
            }
        }
    }
}
