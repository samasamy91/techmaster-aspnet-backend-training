using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill11_DuplicateNumberDetector
    {
        public static void Run()
        {
            List<int> numbers = new List<int>();
            Console.Write("How many numbers do you want to enter? ");
            int count;
            if (int.TryParse(Console.ReadLine(), out count))
            {
                if (count <= 0)
                {
                    Console.WriteLine("List cannot be empty.");
                }
                else
                {
                    for (int i = 1; i <= count; i++)
                    {
                        Console.Write("Enter number " + i + ": ");

                        int number;

                        if (int.TryParse(Console.ReadLine(), out number))
                        {
                            numbers.Add(number);
                        }
                        else
                        {
                            Console.WriteLine("Invalid number.");
                            i--;
                        }
                    }
                    HashSet<int> seen = new HashSet<int>();//store number first seen
                    HashSet<int> duplicates = new HashSet<int>();//store duplicated
                    foreach (int number in numbers)
                    {
                        if (!seen.Add(number))
                        {
                            duplicates.Add(number);
                        }
                    }
                    if (duplicates.Count > 0)
                    {
                        Console.WriteLine("Duplicates: " + string.Join(", ", duplicates));
                    }
                    else
                    {
                        Console.WriteLine("No duplicates found.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }
}
