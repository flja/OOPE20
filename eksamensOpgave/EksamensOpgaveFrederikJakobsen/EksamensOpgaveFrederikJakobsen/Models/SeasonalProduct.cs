using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgave.Util;
using EksamensOpgave.Interfaces;

namespace EksamensOpgave.Models
{
    class SeasonalProduct : Product
    {
        DateTime _seasonEndDate;
        DateTime _seasonStartDate;

        public SeasonalProduct(int id, string name, int price, DateTime startDate,
            DateTime endDate, IValidation validation) : base(id, name, price, false, validation)
        {
            SeasonStartDate = startDate;
            SeasonEndDate = endDate;
        }

        new public bool Active
        {
            get
            {
                if (DateTime.Now >= _seasonStartDate && DateTime.Now <= _seasonEndDate)
                    return true;
                else
                    return false;
            }
        }
        public DateTime SeasonEndDate { get => _seasonEndDate; set => _seasonEndDate = value; }
        public DateTime SeasonStartDate { get => _seasonStartDate; set => _seasonStartDate = value; }
    }
}
