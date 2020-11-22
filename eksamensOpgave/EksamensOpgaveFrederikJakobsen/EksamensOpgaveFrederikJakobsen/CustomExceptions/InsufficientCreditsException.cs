using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgave.Models;

namespace EksamensOpgave.CustomExceptions
{
    class InsufficientCreditsException : Exception
    {
        User user;
        Product product;

        public InsufficientCreditsException(User user, Product product, String message):base(message)
        {
            User = user;
            Product = product;
        }

        internal User User { get => user; private set => user = value; }
        internal Product Product { get => product; set => product = value; }
    }
}
