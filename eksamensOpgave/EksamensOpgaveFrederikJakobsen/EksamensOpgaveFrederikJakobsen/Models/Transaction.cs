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
        int amount;

        public Transaction(User user, int amount)
        {
            User = user; 
            Amount = amount;
        }

        public int Amount { get => amount; set => amount = value; }
        internal User User { get => user; set => user = value; }
        public DateTime TransactionDate { get => transactionDate; private set => transactionDate = value; }

        public override string ToString()
        {
            return $"Id: {id}, Bruger: {User.ToString()}, Beløb: {Amount}, " +
                $"Dato: {TransactionDate.ToString("dd-MM-yyyy HH:mm")}";
        }

        public virtual void Execute()
        {
            TransactionDate = DateTime.Now;
            user.Balance += amount;
        }
    }
}
