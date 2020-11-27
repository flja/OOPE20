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
            Console.WriteLine($"Kunne ikke finde admin commando {adminCommand}");
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine($"En fejl indtræf: {errorString}");
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.WriteLine($"Ikke nok midler tilrådige: {user}\n, ved køb af, {product}");
        }

        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"Produktet med produkt ID: {product}, kunne ikke findes");
        }

        public void DisplayArgumentCountError(string command)
        {
            Console.WriteLine($"Forket antal agrumenter indtastede: {command}");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine(transaction.ToString());
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine($"proukter købt:");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(transaction.ToString());
            }
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine($"Bruger: {user}");
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"Kunne ikke finde bruger med username: {username}");
        }

        public void Start()
        {
            Console.SetWindowSize(idStrLength + productStrLength + priceStrLength + 10, windowHeight);
            DrawMenu();
            stayAlive = true;

            while (stayAlive)
            {
                try
                {
                    CommandEntered?.Invoke(Console.ReadLine());
                }
                catch (Exception ex)
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
            Console.WriteLine($"|{{0,-{idStrLength}}}|{{1,-{productStrLength}}}|{{2,{priceStrLength}}}|", "ID", "Produkter", "Pris");
            CreateLine('-', width);

            foreach (Product p in stregsystem.ActiveProducts)
            {
                Console.WriteLine($"|{{0,-{idStrLength}}}|{{1,-{productStrLength}}}|{{2,{priceStrLength}:N2}}|", p.Id, p.Name, p.Price * 0.01f);
            }
            CreateLine('-', width);
        }

        void CreateLine(char toWrite, int count)
        {
            for (int i = 0; i < count + 4; i++)
            {
                Console.Write(toWrite);
            }
            Console.WriteLine("");
        }

        public void DisplayProductInactive(string productID)
        {
            Console.WriteLine($"Produkt med ID: {productID} er ikke aktivt");
        }

        public void DisplayAdminCommandSucced(string adminCommand)
        {
            Console.WriteLine($"Admin command: {adminCommand} er udført");
        }

        public void DisplayTransaction(Transaction transaction)
        {
            Console.WriteLine(transaction.ToString());
        }

        public void DisplayUserLowOnMoney(User user, decimal amount)
        {
            Console.WriteLine($"FÅ PENGE TILBAGE!!!!\n{user}\nBeløb: {amount}");
        }

        public void DisplayUserInfo(string userInfo)
        {
            Console.WriteLine(userInfo);
        }
    }
}
