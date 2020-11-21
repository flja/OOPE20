using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgaveFrederikJakobsen.Models;

namespace EksamensOpgaveFrederikJakobsen.CustomExceptions
{
    class UserNotFoundException : KeyNotFoundException
    {
        string user;
        public UserNotFoundException(string username, string message) : base(message)
        {
            User = username;
        }

        internal string User { get => user; set => user = value; }
    }
}
