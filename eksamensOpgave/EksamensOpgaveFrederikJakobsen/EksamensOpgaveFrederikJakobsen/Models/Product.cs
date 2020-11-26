using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgave.Util;
using EksamensOpgave.Interfaces;

namespace EksamensOpgave.Models
{
    class Product
    {
        static List<int> _uniqueId = new List<int>();
        int _id;
        string _name;
        int _price;
        bool _active;
        bool _canBeBoughtOnCredit;
        IValidation _validation;

        public Product(int id, string productName, int price, bool active, IValidation validation)
        {
            Id = id;
            Name = productName;
            Price = price;
            Active = active;
            Validation = validation;
        }

        public int Id
        {
            get => _id;
            private set
            {
                if (value > 0)
                {
                    _uniqueId.Add(Validation.UniqueIdChecker(_uniqueId, value));
                    _id = value;
                }
                else
                    throw new ArgumentOutOfRangeException("Number must be 1 or above!");
            }
        }
        public string Name 
        { 
            get => _name;
            set
            {
                if (Validation.ValidateName(value))
                    _name = value;
                else
                    throw new ArgumentOutOfRangeException("Name can not be empty");
            }
        }
        public int Price { get => _price; set => _price = value; }
        public bool Active { get => _active; set => _active = value; }
        public bool CanBeBoughtOnCredit { get => _canBeBoughtOnCredit; set => _canBeBoughtOnCredit = value; }
        protected IValidation Validation { get => _validation; set => _validation = value; }
        public override string ToString()
        {
            return $"Id:{_id}| {_name} | Pris: {_price * 0.01f} DKK";
        }
    }
}
