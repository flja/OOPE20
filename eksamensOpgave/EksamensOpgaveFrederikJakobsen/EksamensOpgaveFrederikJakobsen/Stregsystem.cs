using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using EksamensOpgave.Util;
using EksamensOpgave.Models;
using EksamensOpgave.CustomExceptions;
using EksamensOpgave.Interfaces;

namespace EksamensOpgave
{
    class Stregsystem : IStregsystem
    {
        List<Product> _products = new List<Product>();
        List<User> _users = new List<User>();
        List<Transaction> _transactions = new List<Transaction>();
        IValidation _validation;

        public event EventHandler LowUserBalance;
        public event UserBalanceNotification UserBalanceWarning;

        public Stregsystem()
        {
            _validation = new Validations(
                new Regex(@"^[\w\d]+$"), 
                new Regex(@"^[\w\.\-\,]+@[a-zA-Z0-9][a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]+$")
                );

            LoadUsers();
            LoadProducts();
        }

        private List<User> Users { get => _users; set => _users = _validation.NullCheck(value); }
        private List<Transaction> Transactions { get => _transactions; set => _transactions = _validation.NullCheck(value); }
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
                    user = new User(int.Parse(subLines[0]), subLines[1], subLines[2].ToLower(), subLines[3], subLines[5], int.Parse(subLines[4]), _validation);
                    _users.Add(user);
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
                //HTML remove syntax source : https://www.dotnetperls.com/remove-html-tags
                string s = Regex.Replace(lines[i], "<.*?>", string.Empty);
                subLines = s.Split(new char[] { ';' });
                if (subLines.Length >= 5)
                {
                    product = new Product(
                        int.Parse(subLines[0]), 
                        subLines[1], int.Parse(subLines[2]), 
                        subLines[3] == "1", _validation);
                    product.Name = product.Name.Replace("\"", "");
                    _products.Add(product);
                }
            }
        }
        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            InsertCashTransaction ict = new InsertCashTransaction(user, amount, _validation);
            ict.Execute();
            Transactions.Add(ict);
            return ict;
        }
        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction t = new BuyTransaction(user, product, _validation);
            t.Execute();
            _transactions.Add(t);
            return t;
        }
        public Product GetProductByID(int id)
        {
            Product product = Products.Where(p => p.Id == id).FirstOrDefault();
            if (product == null)
                throw new ProductNotFoundException(id ,"No product with entered ID found");
            return product;
        }
        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return Transactions.OrderByDescending(t => t.TransactionDate).Where(s=> s.User.Equals(user)).Take(count);
        }
        public User GetUsers(Func<User, bool> predicate)
        {
            foreach(User user in _users)
            {
                if (predicate(user))
                    return user;
            }
            throw new UserNotFoundException("","No user was found");
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
        private List<Product> Products { get => _products; set => _products = _validation.NullCheck(value); }
    }
}
