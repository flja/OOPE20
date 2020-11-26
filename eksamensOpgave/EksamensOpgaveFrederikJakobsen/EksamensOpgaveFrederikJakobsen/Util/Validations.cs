using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EksamensOpgave.Interfaces;

namespace EksamensOpgave.Util
{
    
    class Validations : IValidation
    {
        Regex _userNameRegex;
        Regex _emailRegex;
        
        public Validations(Regex userNameRegex, Regex emailRegex)
        {
            _userNameRegex = userNameRegex;
            _emailRegex = emailRegex;
        }
        public bool ValidateName(string name)
        {
            
            if (name?.Length > 0)
                return true;
            else
                return false;
        }
        public bool ValidateUserName(string userName)
        {
            return _userNameRegex.IsMatch(userName);
        }
        public bool ValidateEmail(string email)
        {
            return _emailRegex.IsMatch(email);
        }
        public T NullCheck<T>(T value)
        {
            return value == null ? throw new ArgumentNullException() : value;
        }
        public int UniqueIdChecker(List<int> list, int id)
        {
            return list.Contains(id) == false ? id : throw new ArgumentException();
        }
    }
}
