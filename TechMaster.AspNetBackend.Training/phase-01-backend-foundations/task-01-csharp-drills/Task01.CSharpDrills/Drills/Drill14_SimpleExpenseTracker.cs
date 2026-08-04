using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    class Expense
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
    public class Drill14_SimpleExpenseTracker
    {
        public static void Run()
        {
            List<Expense> expenses = new List<Expense>();
            Console.Write("How many expenses do you want to enter? ");
            int count;
            if (int.TryParse(Console.ReadLine(), out count))
            {
                if (count <= 0)
                {
                    Console.WriteLine("Number of expenses must be greater than 0.");
                }
                else
                {
                    for (int i = 1; i <= count; i++)
                    {
                        Expense expense = new Expense();
                        Console.Write("Enter expense name: ");
                        expense.Name = Console.ReadLine();
                        Console.Write("Enter expense amount: ");
                        decimal amount;
                        if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0)
                        {
                            expense.Amount = amount;
                            expenses.Add(expense);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount.");
                            i--;
                        }
                    }
                    decimal total = 0;
                    decimal highest = expenses[0].Amount;
                    string highestName = expenses[0].Name;
                    foreach (Expense expense in expenses)
                    {
                        total += expense.Amount;
                        if (expense.Amount > highest)
                        {
                            highest = expense.Amount;
                            highestName = expense.Name;
                        }
                    }
                    decimal average = total / expenses.Count;
                    Console.WriteLine();
                    Console.WriteLine("Total: " + total);
                    Console.WriteLine("Average: " + average);
                    Console.WriteLine("Highest Expense: " + highestName + " (" + highest + ")");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }
}
