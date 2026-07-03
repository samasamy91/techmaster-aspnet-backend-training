using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill07_NameFormatter
    {
        public static void Run()
        {
            Console.Write("Enter your full name: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty");
            }
            else
            {
                name = name.Trim(); //remove spaces at begin and end
                string[] parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);//split name
                for(int i = 0; i < parts.Length; i++)
                {
                    parts[i] = parts[i].ToLower();
                    parts[i] = parts[i][0].ToString().ToUpper() + parts[i].Substring(1);
                }
                Console.WriteLine("Formatted Name: " + string.Join(" ", parts));
            }
        }
    }
}
