using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgaveFrederikJakobsen.Util;

namespace EksamensOpgaveFrederikJakobsen.Models
{
    class Product
    {
        int id;
        string name;
        decimal price;
        bool active;
        bool canBeBoughtOnCredit;

        public Product(int id, string productName, decimal price, bool active, bool canBeBoughtOnCredit)
        {
            Id = id;
            Name = productName;
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = canBeBoughtOnCredit;
        }

        public int Id
        {
            get => id;
            set
            {
                if (value >= 1)
                    id = value;
                else
                    throw new ArgumentOutOfRangeException("Number must be 1 or above!");
            }
        }
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
        public bool Active { get => active; set => active = value; }
        public bool CanBeBoughtOnCredit { get => canBeBoughtOnCredit; set => canBeBoughtOnCredit = value; }

        public override string ToString()
        {
            return $"Id:{id}. Produkt: {name}, Pris: {price}";
        }

    }
}
