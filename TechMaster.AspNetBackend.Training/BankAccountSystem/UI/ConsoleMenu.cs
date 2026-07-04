using BankAccountSystem.Services;
using BankAccountSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.UI
{
    public class ConsoleMenu
    {
        private BankService bankService = new BankService();
        public void Start()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("====== TechMaster Bank System ======");
                Console.WriteLine("1. Create Customer Account");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. Transfer Money");
                Console.WriteLine("5. View Account Details");
                Console.WriteLine("6. View Transaction History");
                Console.WriteLine("7. View All Accounts");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();
                switch(choice)
                {
                    case "1":
                        CreateAccount();
                        break;
                    case "2":
                        Deposit();
                        break;
                    case "3":
                        Withdraw();
                        break;
                    case "4":
                        Transfer();
                        break;
                    case "5":
                        ViewAccount();
                        break;
                    case "6":
                        ViewTransactions();
                        break;
                    case "7":
                        ViewAllAccounts();
                        break;
                    case "8":
                        exit=true;
                        Console.WriteLine("Goodbye");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
        private void CreateAccount()
        {
            Console.Write("Full Name: ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Phone: ");
            string phone = Console.ReadLine();
            Console.Write("Initial Balance: ");
            decimal.TryParse(Console.ReadLine(), out decimal balance);
            Console.WriteLine("Account Type:");
            Console.WriteLine("1. Savings");
            Console.WriteLine("2. Current");
            Console.Write("Choose: ");
            string typeChoice = Console.ReadLine();
            AccountType accountType;

            if (typeChoice == "1")
            {
                accountType = AccountType.Savings;
            }
            else if (typeChoice == "2")
            {
                accountType = AccountType.Current;
            }
            else
            {
                Console.WriteLine("Invalid account type.");
                return;
            }

            Customer customer = new Customer
            {
                FullName = name,
                Email = email,
                PhoneNumber = phone
            };
            bool success = bankService.CreateAccount(customer, balance, accountType);

            if (success)
                Console.WriteLine("Account created successfully.");
            else
                Console.WriteLine("Failed to create account.");
        }

        private void Deposit()
        {
            Console.Write("Account Number: ");
            string account = Console.ReadLine();
            Console.Write("Amount: ");
            decimal.TryParse(Console.ReadLine(), out decimal amount);
            if (bankService.Deposit(account, amount))
                Console.WriteLine("Deposit successful.");
            else
                Console.WriteLine("Deposit failed.");
        }
        private void Withdraw()
        {
            Console.Write("Account Number: ");
            string account = Console.ReadLine();
            Console.Write("Amount: ");
            decimal.TryParse(Console.ReadLine(), out decimal amount);
            if (bankService.Withdraw(account, amount))
                Console.WriteLine("Withdrawal successful.");
            else
                Console.WriteLine("Withdrawal failed.");
        }
        private void Transfer()
        {
            Console.Write("Source Account: ");
            string from = Console.ReadLine();
            Console.Write("Destination Account: ");
            string to = Console.ReadLine();
            Console.Write("Amount: ");
            decimal.TryParse(Console.ReadLine(), out decimal amount);
            if (bankService.Transfer(from, to, amount))
                Console.WriteLine("Transfer successful.");
            else
                Console.WriteLine("Transfer failed.");
        }
        private void ViewAccount()
        {
            Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine();
            BankAccount account = bankService.GetAccountDetails(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }
            Console.WriteLine($"Account Number : {account.AccountNumber}");
            Console.WriteLine($"Customer       : {account.Customer.FullName}");
            Console.WriteLine($"Email          : {account.Customer.Email}");
            Console.WriteLine($"Phone          : {account.Customer.PhoneNumber}");
            Console.WriteLine($"Type           : {account.AccountType}");
            Console.WriteLine($"Balance        : {account.Balance}");
            Console.WriteLine($"Created        : {account.CreatedAt}");
            Console.WriteLine($"Active         : {account.IsActive}");
        }
        private void ViewTransactions()
        {
            Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine();
            var transactions = bankService.GetTransHistory(accountNumber);
            if (transactions == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }
            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions yet.");
                return;
            }
            foreach (var t in transactions)
            {
                Console.WriteLine($"Type: {t.TransactionType}");
                Console.WriteLine($"Amount: {t.Amount}");
                Console.WriteLine($"Date: {t.TransactionDate}");
                Console.WriteLine($"Description: {t.Description}");
                Console.WriteLine($"Balance After: {t.BalanceAfterTransaction}");
            }
        }
        private void ViewAllAccounts()
        {
            var accounts = bankService.GetAllAccounts();
            if (accounts.Count == 0)
            {
                Console.WriteLine("No accounts found.");
                return;
            }
            foreach (var account in accounts)
            {
                Console.WriteLine($"Account : {account.AccountNumber}");
                Console.WriteLine($"Customer: {account.Customer.FullName}");
                Console.WriteLine($"Type    : {account.AccountType}");
                Console.WriteLine($"Balance : {account.Balance}");
                Console.WriteLine($"Status  : {(account.IsActive ? "Active" : "Inactive")}");
            }
        }
    }
}
