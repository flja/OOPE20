using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgave.Interfaces;

namespace EksamensOpgave.Models
{
    class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, int amount, IValidation validation)
            : base(user, amount, validation)
        {

        }
        public override string ToString()
        {
            return $"Indbetaling:\n{base.ToString()}";
        }
        public override void Execute()
        {
            User.Balance += Amount;
            base.Execute();
        }

    }
}
