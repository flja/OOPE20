using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgave.Models;

namespace EksamensOpgave.CustomExceptions
{
    class UserNotFoundException : KeyNotFoundException
    {
        string user;
        public UserNotFoundException(string username, string message) : base(message)
        {
            User = username;
        }

        public string User { get => user; private set => user = value; }
    }
}
