using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill13_PalindromeChecker
    {
        public static void Run()
        {
            Console.Write("Enter a word or sentence: ");
            string text = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Input cannot be empty.");
            }
            else
            {
                text = text.Trim().ToLower();
                text = text.Replace(" ", "");
                string reversed = "";
                for (int i = text.Length - 1; i >= 0; i--)
                {
                    reversed += text[i];
                }

                if (text == reversed)
                {
                    Console.WriteLine("Palindrome");
                }
                else
                {
                    Console.WriteLine("Not Palindrome");
                }
            }
        }
    }
}
