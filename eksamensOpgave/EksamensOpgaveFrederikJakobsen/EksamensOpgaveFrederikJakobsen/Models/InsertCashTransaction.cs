using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensOpgaveFrederikJakobsen.Models
{
    class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, decimal amount ):base(user, amount)
        {

        }

        public override string ToString()
        {
            return $"Indbetaling:\n{base.ToString()}";
        }

        //public override void Execute(decimal amount)
        //{
        //    base.Execute(amount);
        //}

    }
}
