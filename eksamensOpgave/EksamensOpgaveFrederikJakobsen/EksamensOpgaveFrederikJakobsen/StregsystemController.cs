using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using EksamensOpgave.Models;
using EksamensOpgave.Interfaces;
using EksamensOpgave.CustomExceptions;

namespace EksamensOpgave
{
    class StregsystemController
    {
        IStregsystem _stregsystem;
        IStregsystemUI _stregsystemUI;
        Dictionary<string, Action<List<string>>> _admincommands = new Dictionary<string, Action<List<string>>>();

        public StregsystemController(IStregsystemUI stregsystemCLI, IStregsystem stregsystem)
        {
            _stregsystem = stregsystem;
            _stregsystemUI = stregsystemCLI;
            _stregsystemUI.CommandEntered += ParseCommand;

            _admincommands.Add(":q", (List<string> args) => stregsystemCLI.Close());
            _admincommands.Add(":quit", (List<string> args) => stregsystemCLI.Close());
            _admincommands.Add(":activate", (List<string> args) => HandleActiveProduct(args));
            _admincommands.Add(":deactivate", (List<string> args) => HandleDeactiveProduct(args));
            _admincommands.Add(":crediton", (List<string> args) => HandleCreditOn(args));
            _admincommands.Add(":creditoff", (List<string> args) => HandleCreditOff(args));
        }

        void ParseCommand(string command)
        {
            if (!string.IsNullOrEmpty(command))
            {
                command = command.ToLower();
                List<string> inputs = command.Split(new char[] { ' ' }).ToList();

                try
                {
                    if (command[0] == ':')
                    {
                        try
                        {
                            _admincommands[inputs[0]](inputs.ToList());
                        }
                        catch (KeyNotFoundException)
                        {
                            _stregsystemUI.DisplayAdminCommandNotFoundMessage(inputs.First());
                        }
                    }
                    else
                    {
                        HandleUserCMD(inputs);
                    }
                }
                catch(ArgumentOutOfRangeException)
                {
                    _stregsystemUI.DisplayArgumentCountError(inputs.ElementAt(0));
                }
            }
            else
            {
                _stregsystemUI.DisplayGeneralError("Ugyldigt input");
            }
        }

        void HandleUserCMD(List<string> args)
        {
            try
            {
                switch (args.Count)
                {
                    case 2:
                        {
                            HandlePurchase(args.ElementAt(0), int.Parse(args.ElementAt(1)), 1);
                            break;
                        }
                    case 3:
                        {
                            HandlePurchase(args.ElementAt(0), int.Parse(args.ElementAt(2)), int.Parse(args.ElementAt(1)));
                            break;
                        }
                    default:
                        {
                            _stregsystemUI.DisplayGeneralError("Ukendt kommando");
                            break;
                        }
                }
            }
            catch (ArgumentException ex)
            {
                _stregsystemUI.DisplayGeneralError("Ugyldige indtastninger");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        void HandlePurchase(string username, int productId, int cnt)
        {
            User user;
            Product product;
            try
            {
                user = _stregsystem.GetUserByUsername(username);
                product = _stregsystem.GetProductByID(productId);
                for (int i = 0; i < cnt; i++)
                {
                    try
                    {
                        BuyTransaction buyTransaction = _stregsystem.BuyProduct(user, product);
                        _stregsystemUI.DisplayUserBuysProduct(buyTransaction);
                    }
                    catch (InaktivProductPurchaseExceptions)
                    {
                        _stregsystemUI.DisplayProductInactive(productId.ToString());
                        break;
                    }
                    catch (InsufficientCreditsException)
                    {
                        _stregsystemUI.DisplayInsufficientCash(user, product);
                        break;
                    }
                }
            }
            catch (ProductNotFoundException ex)
            {
                _stregsystemUI.DisplayProductNotFound(productId.ToString());
                Debug.WriteLine(ex);
            }
            catch (UserNotFoundException ex)
            {
                _stregsystemUI.DisplayUserNotFound(username);
                Debug.WriteLine(ex);
            }
        }

        void HandleActiveProduct(List<string> args)
        {
            HandleActiveDeaktiveProduct(args.ElementAt(0), int.Parse(args.ElementAt(1)), true);
        }

        void HandleDeactiveProduct(List<string> args)
        {
            HandleActiveDeaktiveProduct(args.ElementAt(0), int.Parse(args.ElementAt(1)), false);
        }

        void HandleActiveDeaktiveProduct(string cmd, int productId, bool setActive)
        {
            try
            {
                Product p = _stregsystem.GetProductByID(productId);
                if (p.GetType() != typeof(SeasonalProduct))
                    p.Active = setActive;
                _stregsystemUI.DisplayAdminCommandSucced(cmd);
            }
            catch (ProductNotFoundException)
            {
                _stregsystemUI.DisplayProductNotFound(productId.ToString());
            }
            catch (FormatException)
            {
                _stregsystemUI.DisplayGeneralError("Produkt ID er ikke et tal");
            }
        }

        void HandleCreditOn(List<string> args)
        {
            HandleBuyOnCreditOnOff(args.ElementAt(0), args.ElementAt(1), true);
        }

        void HandleCreditOff(List<string> args)
        {
            HandleBuyOnCreditOnOff(args.ElementAt(0), args.ElementAt(1), false);
        }

        void HandleBuyOnCreditOnOff(string cmd, string productId, bool setActive)
        {
            try
            {
                Product p = _stregsystem.GetProductByID(int.Parse(productId));
                p.CanBeBoughtOnCredit = setActive;
                _stregsystemUI.DisplayAdminCommandSucced(cmd);
            }
            catch (ProductNotFoundException)
            {
                _stregsystemUI.DisplayProductNotFound(productId.ToString());
            }
            catch (FormatException)
            {
                _stregsystemUI.DisplayGeneralError("Product ID was not a number");
            }
        }
    }
}
