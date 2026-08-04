using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill10_SimpleATMMenu
    {
        public static void Run()
        {
            decimal balance = 1000;
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine(" ATM Menu ");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Current Balance: " + balance);
                        break;
                    case "2":
                        Console.Write("Enter deposit amount: ");
                        decimal deposit;
                        if (decimal.TryParse(Console.ReadLine(), out deposit) && deposit > 0)//check valid
                        {
                            balance += deposit;
                            Console.WriteLine("Deposit successful.");
                            Console.WriteLine("Current Balance: " + balance);
                        }
                        else
                        {
                            Console.WriteLine("Invalid deposit amount.");
                        }
                        break;
                    case "3":
                        Console.Write("Enter withdrawal amount: ");
                        decimal withdraw;
                        if (decimal.TryParse(Console.ReadLine(), out withdraw) && withdraw > 0)
                        {
                            if (withdraw <= balance)
                            {
                                balance -= withdraw;
                                Console.WriteLine("Withdrawal successful.");
                                Console.WriteLine("Current Balance: " + balance);
                            }
                            else
                            {
                                Console.WriteLine("Insufficient balance.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid withdrawal amount.");
                        }
                        break;

                    case "4":
                        exit = true;
                        Console.WriteLine("Thank you for using the ATM.");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
