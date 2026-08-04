using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill18_NumberStatistics
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
                    int sum = 0;
                    int positiveCount = 0;
                    int negativeCount = 0;
                    int zeroCount = 0;
                    int max = numbers[0];
                    int min = numbers[0];
                    foreach (int number in numbers)
                    {
                        sum += number;
                        if (number > max)
                        {
                            max = number;
                        }
                        if (number < min)
                        {
                            min = number;
                        }
                        if (number > 0)
                        {
                            positiveCount++;
                        }
                        else if (number < 0)
                        {
                            negativeCount++;
                        }
                        else
                        {
                            zeroCount++;
                        }
                    }
                    double average = (double)sum / numbers.Count;
                    Console.WriteLine();
                    Console.WriteLine("Count: " + numbers.Count);
                    Console.WriteLine("Sum: " + sum);
                    Console.WriteLine("Average: " + average);
                    Console.WriteLine("Maximum: " + max);
                    Console.WriteLine("Minimum: " + min);
                    Console.WriteLine("Positive Numbers: " + positiveCount);
                    Console.WriteLine("Negative Numbers: " + negativeCount);
                    Console.WriteLine("Zero Numbers: " + zeroCount);
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }
}
