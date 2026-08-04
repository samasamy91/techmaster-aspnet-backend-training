using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill05_MaximumAndMinimumFinder
    {
        public static void Run()
        {
            List<int> numbers = new List<int>();
            Console.Write("How many numbers do you want to enter ? ");
            string input = Console.ReadLine();
            int count;
            if (int.TryParse(input, out count)) //check valid number
            {
                if (count <= 0)
                    Console.WriteLine("List cannot be empty");
                else
                {
                    for(int i =1; i <= count; i++)
                    {
                        Console.Write("Enter number "+i +": ");
                        string numinput = Console.ReadLine();
                        int number;
                        if(int.TryParse(numinput,out number))
                        {
                            numbers.Add(number);
                        }
                        else
                        {
                            Console.WriteLine("Invalid number.");
                            i--;
                        }
                    }
                    int max = numbers[0];
                    int min = numbers[0];

                    for (int i = 1; i < numbers.Count; i++)
                    {
                        if (numbers[i] > max)
                        {
                            max = numbers[i];
                        }

                        if (numbers[i] < min)
                        {
                            min = numbers[i];
                        }
                    }
                    Console.WriteLine("Maximum = " + max);
                    Console.WriteLine("Minimum = " + min);
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        } 
        }
}

