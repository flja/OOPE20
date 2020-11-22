using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensOpgave.CustomExceptions
{
    class ProductNotFoundException : KeyNotFoundException
    {
        int productIdNotFound;
        public ProductNotFoundException(int productId, string message):base (message)
        {
            ProductIdNotFound = productId;
        }

        public int ProductIdNotFound { get => productIdNotFound; private set => productIdNotFound = value; }
    }
}
