﻿using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgave.Models;

namespace EksamensOpgave.Interfaces
{
    interface IStregsystem
    {
        IEnumerable<Product> ActiveProducts { get; }
        InsertCashTransaction AddCreditsToAccount(User user, int amount);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetProductByID(int id);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        User GetUsers(Func<User, bool> predicate);
        User GetUserByUsername(string username);
        event UserBalanceNotification UserBalanceWarning;
    }
}
