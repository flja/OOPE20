using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EksamensOpgaveFrederikJakobsen.Util;
using EksamensOpgaveFrederikJakobsen.Models;
using EksamensOpgaveFrederikJakobsen.CustomExceptions;
using EksamensOpgaveFrederikJakobsen.Interfaces;

namespace EksamensOpgaveFrederikJakobsen
{
    class Stregsystem : IStregsystem
    {
        List<Product> products = new List<Product>();
        List<User> users = new List<User>();
        List<Transaction> transactions = new List<Transaction>();

        public event EventHandler LowUserBalance;
        public event UserBalanceNotification UserBalanceWarning;

        public Stregsystem()
        {
            //List<Product> products, List< User > users, List<Transaction> transactions
            //Products = products;
            //Users = users;
            //Transactions = transactions;
        }

        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            User u = Users.Where(u => u.Equals(Users)).FirstOrDefault();
            if (u == null)
                throw new KeyNotFoundException("No user found");
            InsertCashTransaction ict = new InsertCashTransaction(user, amount);
            ict.Execute();
            Transactions.Add(ict);
            return ict;
        }
        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction t = new BuyTransaction(user, product);
            t.Execute();
            return t;
        }
        public Product GetProductByID(int id)
        {
            Product product = Products.Where(p => p.Id == id).FirstOrDefault();
            if (product == null)
                throw new KeyNotFoundException("No product found");
            return product;
        }
        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return Transactions.Where(t => t.User.Equals(user));
        }
        public User GetUsers(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }
        public User GetUserByUsername(string username)
        {
            User user = Users.Where(u => u.Username == username).FirstOrDefault();
            if (user == null)
                throw new UserNotFoundException(username, $"Unable to find {username}");
            return user;
        }
        public IEnumerable<Product> ActiveProducts
        {
            get
            {
                return Products.Where(p => p.Active);
            }
        }
        private List<Product> Products { get => products; set => products = Validations.NullCheck(value); }
        private List<User> Users { get => users; set => users = Validations.NullCheck(value); }
        private List<Transaction> Transactions { get => transactions; set => transactions = Validations.NullCheck(value); }
    }
}
