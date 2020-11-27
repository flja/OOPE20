using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgave.Interfaces;

namespace EksamensOpgave.Models
{
    class Transaction
    {
        static List<int> _uniqueId = new List<int>();
        int _id;
        User _user;
        DateTime _transactionDate;
        int _amount;
        IValidation _validation;

        public Transaction(User user, int amount, IValidation validation)
        {
            _validation = validation;
            User = user;
            Amount = amount;
        }
        public int Amount { get => _amount; private set => _amount = value; }
        internal User User { get => _user; private set => _user = value; }
        public DateTime TransactionDate { get => _transactionDate; private set => _transactionDate = value; }
        public int Id
        {
            get => _id;
            private set
            {
                if (value > 0 && _validation.UniqueIdChecker(_uniqueId, value))
                {
                    _uniqueId.Add(value);
                    _id = value;
                }
                else
                    throw new ArgumentException("ID is invalid");
            }
        }
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
