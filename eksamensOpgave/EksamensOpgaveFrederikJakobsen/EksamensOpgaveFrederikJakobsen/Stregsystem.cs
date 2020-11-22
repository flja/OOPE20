using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
            LoadUsers();
            LoadProducts();
        }

        void LoadUsers()
        {
            User user;
            string[] subLines;
            string[] lines = Properties.Resources.users.Split(new char[] { '\n'});

            for(int i = 1; i < lines.Length; i++)
            {
                string s = lines[i];
                subLines = s.Split(new char[] { ',' });
                if(subLines.Length >= 6)
                {
                    user = new User(int.Parse(subLines[0]), subLines[1], subLines[2], subLines[3], subLines[5], int.Parse(subLines[4]));
                    users.Add(user);
                }
            }
        }

        void LoadProducts()
        {
            Product product;
            string[] subLines;
            string[] lines = Properties.Resources.products.Split(new char[] { '\n' });

            for (int i = 1; i < lines.Length; i++)
            {
                //HTML remove source : https://www.dotnetperls.com/remove-html-tags
                string s = Regex.Replace(lines[i], "<.*?>", string.Empty);
                subLines = s.Split(new char[] { ';' });
                if (subLines.Length >= 5)
                {
                    product = new Product(
                        int.Parse(subLines[0]), 
                        subLines[1], int.Parse(subLines[2]), 
                        subLines[3] == "0" ? true : false);
                    products.Add(product);
                }
            }
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
