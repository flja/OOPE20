using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using EksamensOpgave.Interfaces;
using EksamensOpgave.Models;

namespace EksamensOpgave
{
    class StregsystemCLI : IStregsystemUI
    {
        bool stayAlive;
        string title = "STREGSYSTEM";
        string description = "Syntax til køb: \"[username] [Produkt ID]\"";
        int windowHeight = 35;
        int idStrLength = 5;
        int productStrLength = 50;
        int priceStrLength = 7;
        IStregsystem stregsystem;

        public event StregsystemEvent CommandEntered;

        public StregsystemCLI(IStregsystem system)
        {
            stregsystem = system;
        }

        public void Close()
        {
            stayAlive = false;
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
            Console.SetWindowSize(idStrLength + productStrLength + priceStrLength + 10, windowHeight);
            DrawMenu();
            stayAlive = true;

            while(stayAlive)
            {
                try
                {
                    CommandEntered?.Invoke(Console.ReadLine());
                }
                catch(Exception ex)
                {
                    DrawMenu();
                    Debug.WriteLine(ex);
                }
            }
        }
        
        void DrawMenu()
        {
            Console.Clear();
            int width = idStrLength + productStrLength + priceStrLength;
            //Draw titel
            CreateLine('-', width);
            Console.SetCursorPosition(width / 2 - title.Length / 2, 1);
            Console.WriteLine(title);
            CreateLine(' ', width);

            //Draw description
            Console.SetCursorPosition(width / 2 - description.Length / 2, 3);
            Console.WriteLine(description);
            CreateLine('-', width);

            //Draw product lines
            //https://docs.microsoft.com/en-us/dotnet/standard/base-types/composite-formatting
            Console.WriteLine($"|{{0,-{idStrLength}}}|{{1,-{productStrLength}}}|{{2,{priceStrLength}}}|","ID", "Produkter", "Pris");
            CreateLine('-', width);

            foreach (Product p in stregsystem.ActiveProducts)
            {
                Console.WriteLine($"|{{0,-{idStrLength}}}|{{1,-{productStrLength}}}|{{2,{priceStrLength}:N2}}|", p.Id, p.Name, p.Price * 0.01f);
            }
            CreateLine('-', width);
        }

        void CreateLine(char toWrite, int count)
        {
            for (int i = 0; i < count+4; i++)
            {
                Console.Write(toWrite);
            }
            Console.WriteLine("");
        }
    }
}
