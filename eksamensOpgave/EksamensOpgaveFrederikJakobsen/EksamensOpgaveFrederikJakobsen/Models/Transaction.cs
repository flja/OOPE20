using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensOpgaveFrederikJakobsen.Models
{
    class Transaction
    {
        int id;
        User user;
        DateTime transactionDate;
        decimal amount;

        public Transaction(User user, decimal amount)
        {
            User = user; 
            Amount = amount;
        }

        public decimal Amount { get => amount; set => amount = value; }
        internal User User { get => user; set => user = value; }

        public override string ToString()
        {
            return $"Id: {id}, Bruger: {User.ToString()}, Beløb: {Amount}, " +
                $"Dato: {transactionDate.ToString("dd-MM-yyyy HH:mm")}";
        }

        public void Execute()
        {
            transactionDate = DateTime.Now;
        }
    }
}
