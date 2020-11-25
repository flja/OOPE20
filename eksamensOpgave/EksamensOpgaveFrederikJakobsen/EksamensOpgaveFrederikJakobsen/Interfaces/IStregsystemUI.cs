using System;
using System.Collections.Generic;
using System.Text;
using EksamensOpgave.Models;

namespace EksamensOpgave.Interfaces
{
    delegate void StregsystemEvent(string command);

    interface IStregsystemUI
    {
        void DisplayUserNotFound(string username); 
        void DisplayProductNotFound(string product);
        void DisplayProductInactive(string product);
        void DisplayUserInfo(User user); 
        void DisplayArgumentCountError(string command); 
        void DisplayAdminCommandNotFoundMessage(string adminCommand);
        void DisplayAdminCommandSucced(string adminCommand);
        void DisplayUserBuysProduct(BuyTransaction transaction); 
        void DisplayUserBuysProduct(int count, BuyTransaction transaction); 
        void Close(); void DisplayInsufficientCash(User user, Product product); 
        void DisplayGeneralError(string errorString); 
        void Start();
        event StregsystemEvent CommandEntered;
    }
}
