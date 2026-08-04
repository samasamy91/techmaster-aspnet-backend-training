using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill03_SimpleLoginValidator
    {
        public static void Run()
        {
            string correctUsername = "admin";
            string correctPassword = "1234";
            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine("Enter Username: ");
                string username = Console.ReadLine();

                Console.WriteLine("Enter Password: ");
                string password = Console.ReadLine();

                if(username.Equals(correctUsername,StringComparison.OrdinalIgnoreCase) && password == correctPassword)
                {
                    Console.WriteLine("Login Successful");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid username or password");
                    if (i == 3)
                    {
                        Console.WriteLine("Account locked.Too many failed attempts ");
                    }
                }
            }
        }
    }
}
