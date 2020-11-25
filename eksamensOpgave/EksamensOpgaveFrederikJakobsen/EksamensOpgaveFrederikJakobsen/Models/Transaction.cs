using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensOpgave.Models
{
    class Transaction
    {
        int _id;
        User _user;
        DateTime _transactionDate;
        int _amount;

        public Transaction(User user, int amount)
        {
            User = user; 
            Amount = amount;
        }

        public int Amount { get => _amount; set => _amount = value; }
        internal User User { get => _user; set => _user = value; }
        public DateTime TransactionDate { get => _transactionDate; private set => _transactionDate = value; }
        public int Id { get => _id; private set => _id = value; }

        public override string ToString()
        {
            return $"Transaction:\nId: {Id}, {User}\nBeløb: {Amount} " +
                $"Dato: {TransactionDate.ToString("dd-MM-yyyy HH:mm")}";
        }

        public virtual void Execute()
        {
            TransactionDate = DateTime.Now;
        }
    }
}
