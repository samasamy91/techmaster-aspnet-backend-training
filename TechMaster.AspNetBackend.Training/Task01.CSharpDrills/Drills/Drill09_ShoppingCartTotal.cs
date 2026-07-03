using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill09_ShoppingCartTotal
    { 
        public static void Run()
        {
            decimal total = 0;
            Console.Write("Enter number of items: ");
            string input = Console.ReadLine();
            int itemCount;
            if (int.TryParse(input, out itemCount))
            {
                if (itemCount <= 0)
                {
                    Console.WriteLine("Number of items must be greater than 0.");
                }
                else
                {
                    for (int i = 1; i <= itemCount; i++)
                    {
                        Console.Write("Enter price of item " + i + ": ");
                        decimal price;
                        if (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
                        {
                            Console.WriteLine("Invalid price.");
                            i--;
                            continue;
                        }
                        Console.Write("Enter quantity of item " + i + ": ");
                        int quantity;
                        if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                        {
                            Console.WriteLine("Invalid quantity.");
                            i--;
                            continue;
                        }
                        decimal subtotal = price * quantity;
                        total += subtotal;
                    }
                    decimal discount = 0;
                    if (total > 1000)
                    {
                        discount = total * 0.10m;
                    }
                    decimal finalTotal = total - discount;
                    Console.WriteLine();
                    Console.WriteLine("Grand Total: " + total.ToString("F2"));
                    Console.WriteLine("Discount: " + discount.ToString("F2"));
                    Console.WriteLine("Final Total: " + finalTotal.ToString("F2"));
                }
            }
            else
            {
                Console.WriteLine("Invalid number of items.");
            }
        }
    }
}
