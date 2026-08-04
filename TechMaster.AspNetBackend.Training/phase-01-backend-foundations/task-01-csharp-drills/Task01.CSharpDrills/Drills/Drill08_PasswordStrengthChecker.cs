using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill08_PasswordStrengthChecker
    {
        public static void Run()
        {
            Console.Write("Enter a password: ");

            string password = Console.ReadLine();

            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            List<string> missing = new List<string>();

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                    hasUpper = true;
                else if (char.IsLower(c))
                    hasLower = true;
                else if (char.IsDigit(c))
                    hasDigit = true;
                else
                    hasSpecial = true;
            }

            if (password.Length < 8)
                missing.Add("at least 8 characters");

            if (!hasUpper)
                missing.Add("uppercase");

            if (!hasLower)
                missing.Add("lowercase");

            if (!hasDigit)
                missing.Add("digit");

            if (!hasSpecial)
                missing.Add("special character");

            if (missing.Count == 0)
            {
                Console.WriteLine("Strong");
            }
            else
            {
                Console.WriteLine("Weak - missing " + string.Join(", ", missing));
            }
        }
    }
}
