using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill15_ArrayRotation
    {
        public static void Run()
        {
            Console.Write("How many numbers do you want to enter? ");
            int size;
            if (int.TryParse(Console.ReadLine(), out size))
            {
                if (size <= 0)
                {
                    Console.WriteLine("Array cannot be empty.");
                }
                else
                {
                    int[] numbers = new int[size];
                    for (int i = 0; i < size; i++)
                    {
                        Console.Write("Enter number " + (i + 1) + ": ");
                        if (!int.TryParse(Console.ReadLine(), out numbers[i]))
                        //{
                        //    // Valid input
                        //}
                        //else
                        {
                            Console.WriteLine("Invalid number.");
                            i--;
                        }
                    }
                    int temp = numbers[size - 1];
                    for (int i = size - 1; i > 0; i--)
                    {
                        numbers[i] = numbers[i - 1];
                    }
                    numbers[0] = temp;
                    Console.WriteLine();
                    Console.WriteLine("Array after rotation:");
                    foreach (int number in numbers)
                    {
                        Console.Write(number + " ");
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
