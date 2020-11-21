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

        /// <summary>
        /// Takes a geneic value and throw a null exception if null
        /// </summary>
        /// <typeparam name="T">Geneic type</typeparam>
        /// <param name="value">Value to null check</param>
        /// <returns></returns>
        static public T NullCheck<T>(T value)
        {
            return value == null ? throw new ArgumentNullException() : value;
        }

    }
}
