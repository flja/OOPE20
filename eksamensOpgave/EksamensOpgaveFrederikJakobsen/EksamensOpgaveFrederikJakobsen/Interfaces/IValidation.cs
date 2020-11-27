using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensOpgave.Interfaces
{
    interface IValidation
    {
        public bool ValidateName(string name);
        public bool ValidateUserName(string userName);
        public bool ValidateEmail(string email);
        public T NullCheck<T>(T value);

        public bool UniqueIdChecker(List<int> list, int id);
    }
}
