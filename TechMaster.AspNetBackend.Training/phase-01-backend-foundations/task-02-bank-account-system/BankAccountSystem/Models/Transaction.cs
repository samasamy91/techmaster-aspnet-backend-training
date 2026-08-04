using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; } = Guid.NewGuid();
        public string AccountNumber { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount   { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public decimal BalanceAfterTransaction { get; set; }


    }
}
