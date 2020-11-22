using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgave.CustomExceptions;

namespace EksamensOpgave.Models
{
    class BuyTransaction : Transaction
    {
        Product product;
        public BuyTransaction(User user, Product product) : base(user, product.Price * -1)
        {
            Product = product;
        }

        internal Product Product { get => product; set => product = value; }

        public override string ToString()
        {
            return $"Ordrebekræftelse:\nProdukt: {product.ToString()}\nBruger: {User.ToString()}\nTransaktion: {this.ToString()}";
        }

        public override void Execute()
        {
            if (!product.Active)
                throw new InaktivProductPurchaseExceptions(Product, "Selected product is not available");

            if (User.Balance - Amount < 0)
                if (!product.CanBeBoughtOnCredit)
                    throw new InsufficientCreditsException(User, Product, "User balance insufficient");

            base.Execute();
        }

    }
}
