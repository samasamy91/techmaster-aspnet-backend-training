using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill12_EmailValidator
    {
        public static void Run()
        {
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email cannot be empty.");
            }
            else if (email.Contains(" "))
            {
                Console.WriteLine("Invalid email.");
            }
            else if (!email.Contains("@"))
            {
                Console.WriteLine("Invalid email.");
            }
            else if (!email.Contains("."))
            {
                Console.WriteLine("Invalid email.");
            }
            else if (email.StartsWith("@") || email.EndsWith("@"))
            {
                Console.WriteLine("Invalid email.");
            }
            else
            {
                Console.WriteLine("Valid email.");
            }
        }
    }
}
