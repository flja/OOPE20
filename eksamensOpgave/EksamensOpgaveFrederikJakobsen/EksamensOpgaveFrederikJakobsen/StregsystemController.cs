using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using EksamensOpgave.Models;
using EksamensOpgave.Interfaces;
using EksamensOpgave.CustomExceptions;

namespace EksamensOpgave
{
    class StregsystemController
    {
        IStregsystem stregsystem;
        IStregsystemUI stregsystemUI;

        public StregsystemController(IStregsystemUI stregsystemCLI, IStregsystem stregsystem)
        {
            this.stregsystem = stregsystem;
            this.stregsystemUI = stregsystemCLI;
            stregsystemUI.CommandEntered += ParseCommand;
        }

        void ParseCommand(string cmd)
        {
            if(cmd?.Length > 0)
            {
                cmd = cmd.ToLower();

                switch (cmd[0])
                {
                    case ':':
                        {
                            HandleAdminCMD(cmd);
                            break;
                        }
                    default:
                        {
                            HandleUserCMD(cmd);
                            break;
                        }
                }
            }
            else
            {
                stregsystemUI.DisplayGeneralError("Indtasted var ikke en kommando");
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
                            stregsystemUI.DisplayGeneralError("Ukendt kommando");
                            break;
                        }
                }
            }
            catch(ArgumentException ex)
            {
                stregsystemUI.DisplayGeneralError(" \"ID\" / \"Antal\" skal være et tal");
                Debug.WriteLine(ex);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        void HandlePauchause(string username, int productId, int cnt)
        {
            try
            {
                User user = stregsystem.GetUserByUsername(username);
                Product product = stregsystem.GetProductByID(productId);
                for(int i= 0; i< cnt; i++)
                {
                    try
                    {
                        BuyTransaction buyTransaction = stregsystem.BuyProduct(user, product);
                        stregsystemUI.DisplayUserBuysProduct(buyTransaction);
                    }
                    catch(InaktivProductPurchaseExceptions)
                    {
                        stregsystemUI.DisplayGeneralError($"{product} er ikke længere aktivt");
                    }
                    catch(InsufficientCreditsException)
                    {
                        stregsystemUI.DisplayInsufficientCash(user, product);
                    }
                }
            }
            catch(ProductNotFoundException ex)
            {
                stregsystemUI.DisplayProductNotFound($"Produkt med Id {productId} kunne ikke findes");
                Debug.WriteLine(ex);
            }
            catch(UserNotFoundException ex)
            {
                stregsystemUI.DisplayUserNotFound(username);
                Debug.WriteLine(ex);
            }
        }

        void HandleAdminCMD(string cmd)
        {
            string[] inputs = cmd.Split(new char[] { ' ' });

            switch(inputs.Length)
            {
                case 1:
                    {
                        if (inputs[0] == ":quit" || inputs[0] == ":q")
                            stregsystemUI.Close();
                        break;
                    }
                case 2:
                    {
                        if(inputs[0] == ":activate" || inputs[0] == ":deactivate")
                        {
                            try
                            {
                                int productId = int.Parse(inputs[1]);
                                Product p = stregsystem.GetProductByID(productId);
                                if(p.GetType() != typeof(SeasonalProduct))
                                    p.Active = inputs[0] == ":activate" ? true : false;
                            }
                            catch(ProductNotFoundException)
                            {
                                stregsystemUI.DisplayProductNotFound(inputs[1]);
                            }
                            catch(FormatException)
                            {
                                stregsystemUI.DisplayGeneralError("Product ID was not a number");
                            }
                        }
                        else if (inputs[0] == ":crediton" || inputs[0] == ":creditoff")
                        {
                            try
                            {
                                int productId = int.Parse(inputs[1]);
                                Product p = stregsystem.GetProductByID(productId);
                                    p.CanBeBoughtOnCredit = inputs[0] == ":crediton" ? true : false;
                            }
                            catch (ProductNotFoundException)
                            {
                                stregsystemUI.DisplayProductNotFound(inputs[1]);
                            }
                            catch (FormatException)
                            {
                                stregsystemUI.DisplayGeneralError("Product ID was not a number");
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        if(inputs[0] == ":addcredits")
                        {
                            try
                            {
                                int value = int.Parse(inputs[2]);
                                stregsystem.GetUserByUsername(inputs[1]).Balance += value;
                            }
                            catch(UserNotFoundException)
                            {
                                stregsystemUI.DisplayUserNotFound(inputs[1]);
                            }
                            catch(FormatException)
                            {
                                stregsystemUI.DisplayGeneralError("Product ID was not a number");
                            }
                        }
                        break;
                    }
                default:
                    stregsystemUI.DisplayAdminCommandNotFoundMessage(inputs[0]);
                    break;
            }
        }

    }
}
