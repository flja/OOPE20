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

            _admincommands.Add(":q", (List<string> cmd) => stregsystemCLI.Close());
            _admincommands.Add(":quit", (List<string> cmd) => stregsystemCLI.Close());
            _admincommands.Add(":activate", (List<string> cmd) => HandleActiveDeaktiveProducts(cmd));
            _admincommands.Add(":deactivate", (List<string> cmd) => HandleActiveDeaktiveProducts(cmd));
            _admincommands.Add(":crediton", (List<string> cmd) => HandleCreditOn(cmd));
            _admincommands.Add(":creditoff", (List<string> cmd) => HandleCreditOff(cmd));
        }

        void ParseCommand(string command)
        {
            if (command?.Length > 0)
            {
                command = command.ToLower();
                string[] inputs = command.Split(new char[] { ' ' });

                try
                {
                    _admincommands[inputs[0]](inputs.ToList());
                }
                catch (KeyNotFoundException)
                {
                    HandleUserCMD(command);
                }
            }
            else
            {
                _stregsystemUI.DisplayGeneralError("Indtasted var ikke en kommando");
            }
        }

        void HandleUserCMD(string cmd)
        {
            string[] inputs = cmd.Split(new char[] { ' ' });

            try
            {
                switch (inputs.Length)
                {
                    case 2:
                        {
                            HandlePauchause(inputs[0], int.Parse(inputs[1]), 1);
                            break;
                        }
                    case 3:
                        {
                            HandlePauchause(inputs[0], int.Parse(inputs[2]), int.Parse(inputs[1]));
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
                _stregsystemUI.DisplayGeneralError(" \"ID\" / \"Antal\" skal være et tal");
                Debug.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        void HandlePauchause(string username, int productId, int cnt)
        {
            try
            {
                User user = _stregsystem.GetUserByUsername(username);
                Product product = _stregsystem.GetProductByID(productId);
                for (int i = 0; i < cnt; i++)
                {
                    try
                    {
                        BuyTransaction buyTransaction = _stregsystem.BuyProduct(user, product);
                        _stregsystemUI.DisplayUserBuysProduct(buyTransaction);
                    }
                    catch (InaktivProductPurchaseExceptions)
                    {
                        _stregsystemUI.DisplayGeneralError($"{product} er ikke længere aktivt");
                    }
                    catch (InsufficientCreditsException)
                    {
                        _stregsystemUI.DisplayInsufficientCash(user, product);
                    }
                }
            }
            catch (ProductNotFoundException ex)
            {
                _stregsystemUI.DisplayProductNotFound($"Produkt med Id {productId} kunne ikke findes");
                Debug.WriteLine(ex);
            }
            catch (UserNotFoundException ex)
            {
                _stregsystemUI.DisplayUserNotFound(username);
                Debug.WriteLine(ex);
            }
        }

        void HandleActiveDeaktiveProducts(List<string> cmds)
        {
            try
            {
                Product p = _stregsystem.GetProductByID(int.Parse(cmds[1]));
                if (p.GetType() != typeof(SeasonalProduct))
                    p.Active = cmds[0] == ":activate" ? true : false;
            }
            catch (ProductNotFoundException)
            {
                _stregsystemUI.DisplayProductNotFound(cmds[1]);
            }
            catch (FormatException)
            {
                _stregsystemUI.DisplayGeneralError("Product ID was not a number");
            }
        }

        void HandleCreditOn(List<string> cmds)
        {
            try
            {
                HandleBuyOnCreditOnOff(cmds[1], true);
            }
            catch (ArgumentOutOfRangeException)
            {
                _stregsystemUI.DisplayArgumentCountError($"Forventede 2 værdier men {cmds.Count} modtaget");
            }
        }

        void HandleCreditOff(List<string> cmds)
        {
            try
            {
                HandleBuyOnCreditOnOff(cmds[1], false);
            }
            catch (ArgumentOutOfRangeException)
            {
                _stregsystemUI.DisplayArgumentCountError($"Forventede 2 værdier men {cmds.Count} modtaget");
            }
        }

        void HandleBuyOnCreditOnOff(string productId, bool setActive)
        {
            try
            {
                Product p = _stregsystem.GetProductByID(int.Parse(productId));
                p.CanBeBoughtOnCredit = setActive;
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
