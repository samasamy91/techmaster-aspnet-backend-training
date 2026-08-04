using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill16_FrequencyCounter
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
                    Dictionary<int, int> frequency = new Dictionary<int, int>();
                    foreach (int number in numbers)
                    {
                        if (frequency.ContainsKey(number))
                        {
                            frequency[number]++;
                        }
                        else
                        {
                            frequency.Add(number, 1);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Frequency:");
                    foreach (KeyValuePair<int, int> item in frequency)
                    {
                        Console.WriteLine(item.Key + " => " + item.Value);
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
