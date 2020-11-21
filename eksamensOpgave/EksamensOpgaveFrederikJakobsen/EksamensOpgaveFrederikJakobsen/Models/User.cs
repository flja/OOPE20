using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgaveFrederikJakobsen.Util;

namespace EksamensOpgaveFrederikJakobsen.Models
{
    class User : IComparable
    {
        int id;
        string firstName;
        string lastName;
        string username;
        string email;
        decimal balance;

        public User(int id, string firstName, string lastName, 
            string userName, string email, decimal balance)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
            Balance = balance;
        }

        public decimal Balance1 { get => balance; set => balance = value; }

        int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value >= 1)
                    id = value;
                else
                    throw new ArgumentOutOfRangeException("Number must be 1 or above!");
            }
        }
        public string FirstName 
        {
            get
            {
                return firstName;
            }
            set
            {
                if (Validations.ValidateName(value))
                    firstName = value;
                else
                    throw new ArgumentOutOfRangeException("Name can not be empty");
            }
        }
        public string LastName 
        { 
            get
            {
                return lastName;
            }
            set
            {
                if (Validations.ValidateName(value))
                    lastName = value;
                else
                    throw new ArgumentOutOfRangeException("Name can not be empty");
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (Validations.ValidateUserName(value))
                    username = value;
                else
                    throw new ArgumentOutOfRangeException("Username can only contain [0-9], [a-z], '_'");
            }
        }
        public string Email 
        {
            get
            {
                return email;
            }
            set
            {
                if (Validations.ValidateEmail(value))
                    email = value;
                else
                    throw new ArgumentOutOfRangeException("");
            }
        }
        public decimal Balance
        {
            get
            {
                return Balance1;
            }
            set
            {
                Balance1 = value;
            }
        }

        public int CompareTo(object obj)
        {
            return id.CompareTo(obj);
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
            return $"{firstName} {lastName} ({email})";
        }
    }
}
