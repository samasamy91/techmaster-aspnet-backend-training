using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill19_SimpleTicketPriceCalculator
    {
        public static void Run()
        {
            decimal basePrice = 100;
            decimal discount = 0;
            Console.Write("Enter your age: ");
            int age;
            if (int.TryParse(Console.ReadLine(), out age))
            {
                if (age < 0)
                {
                    Console.WriteLine("Invalid age.");
                }
                else
                {
                    Console.Write("Are you a student? (yes/no): ");
                    string student = (Console.ReadLine() ?? "").ToLower();
                    if (age < 12)
                    {
                        discount = 0.50m;
                    }
                    if (age > 60 && discount < 0.30m)
                    {
                        discount = 0.30m;
                    }
                    if (student == "yes" && discount < 0.20m)
                    {
                        discount = 0.20m;
                    }
                    decimal finalPrice = basePrice * (1 - discount);
                    Console.WriteLine("Ticket Price: " + finalPrice);
                }
            }
            else
            {
                Console.WriteLine("Invalid age.");
            }
        }
    }
}
