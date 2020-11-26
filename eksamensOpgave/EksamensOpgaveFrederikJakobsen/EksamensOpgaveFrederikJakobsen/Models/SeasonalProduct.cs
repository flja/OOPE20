using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgave.Util;
using EksamensOpgave.Interfaces;

namespace EksamensOpgave.Models
{
    class SeasonalProduct : Product
    {
        DateTime seasonEndDate;
        DateTime seasonStartDate;

        public SeasonalProduct(int id, string name, int price, DateTime startDate, 
            DateTime endDate, IValidation validation) : base (id, name, price, false, validation)
        {
            SeasonStartDate = startDate;
            SeasonEndDate = endDate;
        }

        new public bool Active
        {
            get
            {
                if (DateTime.Now >= seasonStartDate && DateTime.Now <= seasonEndDate)
                    return true;
                else
                    return false;
            }
        }
        public DateTime SeasonEndDate { get => seasonEndDate; set => seasonEndDate = value; }
        public DateTime SeasonStartDate { get => seasonStartDate; set => seasonStartDate = value; }
    }
}
