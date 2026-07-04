using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.Models
{
    public class BankAccount
    {
        public string AccountNumber { get; set; }
        public Customer Customer { get; set; }
        public decimal Balance { get; private set; } //cant change 
        public AccountType AccountType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true; 
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public void SetInitialBalance(decimal amount) //assign
        {
            Balance = amount;
        }
        public void Deposit(decimal amount)
        {
            Balance += amount;
        }
        public bool Withdraw(decimal amount)
        {
            if (amount > Balance)
                return false;

            Balance -= amount;
            return true;
        }
    }
}
