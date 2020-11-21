using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgaveFrederikJakobsen.Interfaces;
using EksamensOpgaveFrederikJakobsen.Models;

namespace EksamensOpgaveFrederikJakobsen
{
    class StregsystemCLI : IStregsystemUI
    {
        public StregsystemCLI(IStregsystem system)
        {

        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            throw new NotImplementedException();
        }

        public void DisplayGeneralError(string errorString)
        {
            throw new NotImplementedException();
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            throw new NotImplementedException();
        }

        public void DisplayProductNotFound(string product)
        {
            throw new NotImplementedException();
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserInfo(User user)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserNotFound(string username)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            Console.WriteLine("her");
            Console.ReadKey();
            throw new NotImplementedException();
        }
    }
}
