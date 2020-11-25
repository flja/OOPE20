using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgave.Util;

namespace EksamensOpgave.Models
{
    delegate void UserBalanceNotification(User user, decimal balance);
    

    class User : IComparable
    {
        int _id;
        string _firstName;
        string _lastName;
        string _username;
        string _email;
        int _balance;
        public UserBalanceNotification UserBalanceNotification;

        public User(int id, string firstName, string lastName, 
            string username, string email, int balance)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
            Balance = balance;
        }

        int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value >= 1)
                    _id = value;
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
                if (Validations.ValidateName(value))
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
                if (Validations.ValidateName(value))
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
                if (Validations.ValidateUserName(value))
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
                if (Validations.ValidateEmail(value))
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
            if (_balance < 50)
                UserBalanceNotification?.Invoke(this, _balance);
        }

        public int CompareTo(object obj)
        {
            return _id.CompareTo(obj);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
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
