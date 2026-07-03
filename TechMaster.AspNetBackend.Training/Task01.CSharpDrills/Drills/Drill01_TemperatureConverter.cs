using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill01_TemperatureConverter
    {
        public static void Run()
        {
            Console.Write("Enter temp in Celsuis ");
            string input = Console.ReadLine();
            double celsius;
            if(double.TryParse(input, out celsius))
            {
                double fahrenheit = (celsius * 9 / 5) + 32;
                Console.WriteLine(celsius + "°C = " + fahrenheit.ToString("F2")+ "°F");
            }
            else
            {
                Console.WriteLine("Invalid temp value");
            }

        }
    }
}
