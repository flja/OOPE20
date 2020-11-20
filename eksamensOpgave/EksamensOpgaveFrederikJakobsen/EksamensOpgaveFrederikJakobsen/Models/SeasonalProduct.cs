using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgaveFrederikJakobsen.Util;

namespace EksamensOpgaveFrederikJakobsen.Models
{
    class SeasonalProduct
    {
        int id;
        string name;
        decimal price;
        bool canBeboughtOnCredit;
        DateTime seasonEndDate;
        DateTime seasonStartDate;

        public SeasonalProduct(int id, string name, decimal price, bool canBeBoughtOnCredit,
            DateTime startDate, DateTime endDate)
        {

        }

        public bool Active
        {
            get
            {
                if (DateTime.Now >= seasonStartDate && DateTime.Now <= seasonEndDate)
                    return true;
                else
                    return false;
            }
        }

        public int Id { get => id; set => id = value; }
        public string Name 
        { 
            get => name;
            set
            {
                if (Validations.ValidateName(value))
                    name = value;
                else
                    throw new ArgumentOutOfRangeException("Name can not be empty");
            }
        }
        public decimal Price { get => price; set => price = value; }
        public bool CanBeboughtOnCredit { get => canBeboughtOnCredit; set => canBeboughtOnCredit = value; }
        public DateTime SeasonEndDate { get => seasonEndDate; set => seasonEndDate = value; }
        public DateTime SeasonStartDate { get => seasonStartDate; set => seasonStartDate = value; }
    }
}
