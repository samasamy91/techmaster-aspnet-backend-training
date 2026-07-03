using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill04_EvenOddAnalyzer
    {
        public static void Run()
        {
            List<int> even = new List<int>();
            List<int> odd = new List<int>();
            Console.Write("How many numbers do you want to enter ? ");
            string input = Console.ReadLine();
            int count;
            if (int.TryParse(input, out count)) //check valid number
            {
                if (count <= 0)
                    Console.WriteLine("Count must be greater than 0");
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        Console.Write("Enter number " + i + ": ");
                        string numInput = Console.ReadLine();
                        int num;
                        if (int.TryParse(numInput, out num)) //check that it is int
                        {
                            if (num % 2 == 0)
                                even.Add(num);
                            else
                                odd.Add(num);
                        }
                        else
                        {
                            Console.WriteLine("Invalid number ");
                            i--;//ask for same num again
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Even Numbers: " + string.Join(",", even));
                    Console.WriteLine("Odd Numbers: " + string.Join(",", odd));
                    Console.WriteLine("Even Counts: " + even.Count);
                    Console.WriteLine("Odd Counts: " + odd.Count);
                }
            }
            else
                Console.WriteLine("Invalid count");
        }

    }
}
