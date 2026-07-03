using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill02_GradeCalculator
    {
        public static void Run()
        {
            Console.Write("Enter your score (0-100): ");
            string input = Console.ReadLine();
            int score;
            if (int.TryParse(input, out score))
            {
                if (score < 0 || score > 100)
                    Console.WriteLine("Score must be between 0-100");
                else if (score >= 90)
                    Console.WriteLine("Grade : A");
                else if (score >= 80)
                    Console.WriteLine("Grade : B");
                else if (score >= 70)
                    Console.WriteLine("Grade : C");
                else if (score >= 60)
                    Console.WriteLine("Grade : D");
                else
                    Console.WriteLine("Grade : F");
            }
            else
                Console.WriteLine("Invalid score");
        }
    }
}
