using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgaveFrederikJakobsen.Models;

namespace EksamensOpgaveFrederikJakobsen.CustomExceptions
{
    class InaktivProductPurchaseExceptions : Exception
    {
        Product product;
        public InaktivProductPurchaseExceptions(Product product, string message):base(message)
        {
            Product = product;
        }

        internal Product Product { get => product; set => product = value; }
    }
}
