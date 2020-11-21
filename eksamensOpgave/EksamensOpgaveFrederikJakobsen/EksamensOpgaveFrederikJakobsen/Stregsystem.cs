using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EksamensOpgaveFrederikJakobsen.Models;
using EksamensOpgaveFrederikJakobsen.CustomExceptions;
using EksamensOpgaveFrederikJakobsen.Interfaces;

namespace EksamensOpgaveFrederikJakobsen
{
    class Stregsystem : IStregsystem
    {
        List<Product> products;
        List<User> users;
        List<Transaction> transactions;

        public event EventHandler LowUserBalance;
        public event UserBalanceNotification UserBalanceWarning;

        public Stregsystem()
        {

        }

        void BuyProduct(User user, Product product)
        {

        }

        void AddCreditsToAccount(User user, decimal amount)
        {

        }

        void ExecuteTransaction(Transaction transaction)
        {

        }

        Product GetProductByID(int id)
        {
            return null;
        }

        User GetUserByUsername(string username)
        {
            return null;
        }

        Transaction GetTransactions(User user, int count)
        {
            return null;
        }

        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
           User u = users.Where(u => u.Equals(users)).FirstOrDefault();
            if (u == null)
                throw new KeyNotFoundException("No user found");
            InsertCashTransaction ict = new InsertCashTransaction(user, amount);
            ict.Execute();
            transactions.Add(ict);
            return ict;
        }

        BuyTransaction IStregsystem.BuyProduct(User user, Product product)
        {
            //BuyProduct(user, product);
            BuyTransaction t = new BuyTransaction(user, product);
            t.Execute();
            return t;
        }

        Product IStregsystem.GetProductByID(int id)
        {
            Product product = products.Where(p => p.Id == id).FirstOrDefault();
            if (product == null)
                throw new KeyNotFoundException("No product found");
            return product;
        }

        IEnumerable<Transaction> IStregsystem.GetTransactions(User user, int count)
        {
            return transactions.Where(t => t.User.Equals(user));
        }

        public User GetUsers(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        User IStregsystem.GetUserByUsername(string username)
        {
            User user = users.Where(u => u.Username == username).FirstOrDefault();
            if (user == null)
                throw new UserNotFoundException(username, $"Unable to find {username}");
            return user;
        }

        public List<Product> ActiveProducts
        {
            get
            {
                return products.Where(p => p.Active).ToList();
            }
        }

        IEnumerable<Product> IStregsystem.ActiveProducts => throw new NotImplementedException();
    }
}
