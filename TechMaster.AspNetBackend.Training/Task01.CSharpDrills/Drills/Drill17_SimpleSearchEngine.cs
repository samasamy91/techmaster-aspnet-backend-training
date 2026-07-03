using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill17_SimpleSearchEngine
    {
        public static void Run()
        {
            List<string> names = new List<string>()
            {
                "Sama Samy",
                "Rowida Gamal",
                "Zeina Abdelhameed",
                "Roaa Ahmed",
                "Shahd Mohsen"
            };
            Console.Write("Enter search keyword: ");
            string keyword = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("Keyword cannot be empty.");
            }
            else
            {
                keyword = keyword.ToLower();
                bool found = false;
                Console.WriteLine("Search Results:");
                foreach (string name in names)
                {
                    if (name.ToLower().Contains(keyword))
                    {
                        Console.WriteLine(name);
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("No results found.");
                }
            }
        }
    }
}
