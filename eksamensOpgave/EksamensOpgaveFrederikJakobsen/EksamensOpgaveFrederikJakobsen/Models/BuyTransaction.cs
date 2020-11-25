using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgave.CustomExceptions;

namespace EksamensOpgave.Models
{
    class BuyTransaction : Transaction
    {
        Product _product;
        public BuyTransaction(User user, Product product) : base(user, product.Price)
        {
            Product = product;
        }

        internal Product Product { get => _product; set => _product = value; }

        public override string ToString()
        {
            return $"Købt:\n{_product}\n{base.ToString()}";
        }

        public override void Execute()
        {
            if (!_product.Active)
                throw new InaktivProductPurchaseExceptions(Product, "Selected product is not available");

            if (User.Balance - Amount < 0)
                if (!_product.CanBeBoughtOnCredit)
                    throw new InsufficientCreditsException(User, Product, "User balance insufficient");
            User.Balance -= Product.Price;
            base.Execute();
        }

    }
}
