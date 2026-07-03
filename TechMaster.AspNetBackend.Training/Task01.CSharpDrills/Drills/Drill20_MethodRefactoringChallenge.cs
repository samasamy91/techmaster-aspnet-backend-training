using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.CSharpDrills.Drills
{
    public class Drill20_MethodRefactoringChallenge
    {
        public static void Run()
        {
            Console.WriteLine(" Drill 20 - Method Refactoring Challenge ");
            Console.WriteLine("1. Grade Calculator");
            Console.WriteLine("2. Shopping Cart");
            Console.WriteLine("3. ATM");
            Console.Write("Choose: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    RunGradeCalculator();
                    break;
                case "2":
                    RunShoppingCart();
                    break;
                case "3":
                    RunATM();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        // Drill 02 - Grade Calculator

        static void RunGradeCalculator()
        {
            int score = ReadScore();

            if (!ValidateScore(score))
            {
                Console.WriteLine("Score must be between 0 and 100.");
                return;
            }
            string grade = CalculateGrade(score);
            PrintGrade(grade);
        }
        static int ReadScore()
        {
            Console.Write("Enter score: ");
            int.TryParse(Console.ReadLine(), out int score);
            return score;
        }
        static bool ValidateScore(int score)
        {
            return score >= 0 && score <= 100;
        }
        static string CalculateGrade(int score)
        {
            if (score >= 90)
                return "A";
            else if (score >= 80)
                return "B";
            else if (score >= 70)
                return "C";
            else if (score >= 60)
                return "D";
            else
                return "F";
        }
        static void PrintGrade(string grade)
        {
            Console.WriteLine("Grade: " + grade);
        }

        // Drill 09 - Shopping Cart

        static void RunShoppingCart()
        {
            decimal total = ReadItems();
            decimal discount = CalculateDiscount(total);
            PrintReceipt(total, discount);
        }
        static decimal ReadItems()
        {
            Console.Write("How many items? ");
            int.TryParse(Console.ReadLine(), out int count);
            decimal total = 0;
            for (int i = 1; i <= count; i++)
            {
                decimal price;
                int quantity;
                Console.Write("Price: ");
                decimal.TryParse(Console.ReadLine(), out price);
                Console.Write("Quantity: ");
                int.TryParse(Console.ReadLine(), out quantity);
                total += price * quantity;
            }
            return total;
        }
        static decimal CalculateDiscount(decimal total)
        {
            if (total > 1000)
                return total * 0.10m;
            return 0;
        }
        static void PrintReceipt(decimal total, decimal discount)
        {
            Console.WriteLine("Total: " + total);
            Console.WriteLine("Discount: " + discount);
            Console.WriteLine("Final: " + (total - discount));
        }

        // Drill 10 - ATM

        static void RunATM()
        {
            decimal balance = 1000;
            bool exit = false;
            while (!exit)
            {
                ShowMenu();
                Console.Write("Choose: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        PrintBalance(balance);
                        break;

                    case "2":
                        Deposit(ref balance);
                        break;

                    case "3":
                        Withdraw(ref balance);
                        break;

                    case "4":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
        static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Exit");
        }
        static void PrintBalance(decimal balance)
        {
            Console.WriteLine("Balance: " + balance);
        }
        static void Deposit(ref decimal balance)
        {
            Console.Write("Deposit amount: ");

            decimal.TryParse(Console.ReadLine(), out decimal amount);

            if (amount > 0)
            {
                balance += amount;
                Console.WriteLine("Deposit successful.");
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }
        static void Withdraw(ref decimal balance)
        {
            Console.Write("Withdraw amount: ");

            decimal.TryParse(Console.ReadLine(), out decimal amount);

            if (amount <= 0)
            {
                Console.WriteLine("Invalid amount.");
            }
            else if (amount > balance)
            {
                Console.WriteLine("Insufficient balance.");
            }
            else
            {
                balance -= amount;
                Console.WriteLine("Withdrawal successful.");
            }
        }

    }
}
