using BankAccountSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.Services
{
    public class BankService
    {
        private List<BankAccount> accounts = new List<BankAccount>();
        private int nextAccountNumber = 1001;
        public List<BankAccount> GetAllAccounts()
        {
            return accounts;
        }
        public BankAccount? FindAccount(string accountNumber)
        {
            foreach(var account in accounts)
            {
                if(account.AccountNumber == accountNumber) 
                    return account;
            }
            return null;
        }
        public bool CreateAccount(Customer customer,decimal initialBalance,AccountType accountType)
        {
            if (initialBalance < 0)
                return false;
            BankAccount account = new BankAccount();
            account.AccountNumber = nextAccountNumber.ToString();
            nextAccountNumber++;
            account.AccountType = accountType;
            account.Customer = customer;
            account.SetInitialBalance(initialBalance);
            accounts.Add(account);
            return true;
        } 
        public bool Deposit(string accountNumber, decimal amount)
        {
            BankAccount? account = FindAccount(accountNumber);
            if (account == null) 
                return false;
            if(amount <= 0)
                return false;
            account.Deposit(amount);
            Transaction transaction = new Transaction();
            transaction.AccountNumber = account.AccountNumber;
            transaction.Amount = amount;
            transaction.TransactionType=TransactionType.Deposit;
            transaction.Description = "Cash Deposite";
            transaction.BalanceAfterTransaction = account.Balance;
            account.Transactions.Add(transaction);
            return true;
        }
        public bool Withdraw(string accountNumber,decimal amount)
        {
            BankAccount? account = FindAccount(accountNumber);
            if (account == null)
                return false;
            if (amount <= 0)
                return false;
            if (!account.Withdraw(amount))
                return false;
            Transaction transaction = new Transaction();
            transaction.AccountNumber = account.AccountNumber;
            transaction.Amount = amount;
            transaction.TransactionType = TransactionType.Withdraw;
            transaction.Description = "Cah Withdrawal";
            transaction.BalanceAfterTransaction = account.Balance;
            account.Transactions.Add(transaction);
            return true;

        }
        public bool Transfer(string fromAccount,string toAccount,decimal amount)
        {
            BankAccount? source = FindAccount(fromAccount);
            BankAccount? destination = FindAccount(toAccount);
            if (source == null || destination == null)
                return false;
            if(fromAccount==toAccount)
                return false;
            if (amount <= 0)
                return false;
            if(!source.Withdraw(amount)) 
                return false;
            destination.Deposit(amount);
            Transaction transferOut = new Transaction
            {
                AccountNumber = source.AccountNumber,
                Amount = amount,
                TransactionType = TransactionType.TransferOut,
                Description = "Transfer to " + destination.AccountNumber,
                BalanceAfterTransaction = source.Balance
            };
            Transaction transferIn = new Transaction
            {
                AccountNumber = destination.AccountNumber,
                Amount = amount,
                TransactionType = TransactionType.TransferIn,
                Description = "Transfer from " + source.AccountNumber,
                BalanceAfterTransaction = destination.Balance
            };
            source.Transactions.Add(transferOut);
            destination.Transactions.Add(transferIn);
            return true;
        }
        public BankAccount? GetAccountDetails(string accountNumber)
        {
            return FindAccount(accountNumber);
        }
        public List<Transaction>? GetTransHistory(string accountNumber)
        {
            BankAccount? account = FindAccount(accountNumber);

            if (account == null)
                return null;

            return account.Transactions
                          .OrderByDescending(t => t.TransactionDate)
                          .ToList();
        }
        

    }
}
