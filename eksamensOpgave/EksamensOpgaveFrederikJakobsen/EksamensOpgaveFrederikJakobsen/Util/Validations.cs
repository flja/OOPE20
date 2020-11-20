using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensOpgaveFrederikJakobsen.Util
{
    
    public static class Validations
    {
        static public bool ValidateName(string name)
        {
            if (name?.Length > 0)
                return true;
            else
                return false;
        }
        static public bool ValidateUserName(string userName)
        {
            return true;
        }
        static public bool ValidateEmail(string email)
        {
            return true;
        }
    }
}
