using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgave.Util;
using EksamensOpgave.Interfaces;

namespace EksamensOpgave.Models
{
    delegate void UserBalanceNotification(User user, decimal balance);
    

    class User : IComparable
    {
        static List<int> _uniqueId = new List<int>();

        int _id;
        string _firstName;
        string _lastName;
        string _username;
        string _email;
        int _balance;
        IValidation _validation;
        public UserBalanceNotification UserBalanceNotification;

        public User(int id, string firstName, string lastName, 
            string username, string email, int balance, IValidation validation)
        {
            _validation = validation;
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
            Balance = balance;
        }

        public int Id
        {
            get
            {
                return _id;
            }
            private set
            {
                if (value > 0)
                {
                    _uniqueId.Add(_validation.UniqueIdChecker(_uniqueId, value));
                    _id = value;
                }
                else
                    throw new ArgumentOutOfRangeException("Number must be 1 or above!");
            }
        }
        public string FirstName 
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (_validation.ValidateName(value))
                    _firstName = value;
                else
                    throw new ArgumentOutOfRangeException("Name can not be empty");
            }
        }
        public string LastName 
        { 
            get
            {
                return _lastName;
            }
            set
            {
                if (_validation.ValidateName(value))
                    _lastName = value;
                else
                    throw new ArgumentOutOfRangeException("Name can not be empty");
            }
        }
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (_validation.ValidateUserName(value))
                    _username = value;
                else
                    throw new ArgumentOutOfRangeException("Username can only contain [0-9], [a-z], '_'");
            }
        }
        public string Email 
        {
            get
            {
                return _email;
            }
            set
            {
                if (_validation.ValidateEmail(value))
                    _email = value;
                else
                    throw new ArgumentOutOfRangeException("");
            }
        }
        public int Balance
        {
            get
            {
                CheckForLowBalance();
                return _balance;
            }
            set
            {
                _balance = value;
                CheckForLowBalance();
            }
        }
        private void CheckForLowBalance()
        {
            if (_balance < 5000)
                UserBalanceNotification?.Invoke(this, _balance);
        }
        public int CompareTo(object obj)
        {
            return Id.CompareTo(obj);
        }
        public override bool Equals(object obj)
        {
            return this.Id == ((User)obj).Id;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"{_firstName} {_lastName} {_email}";
        }
    }
}
