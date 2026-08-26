using CruzadorBankGit.Entity;
using CruzadorBankGit.Service;
using CruzadorBankGit.Viewer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.Viewer
{
    internal class ViewerManager
    {
        private readonly AccountService _accountService;
        private readonly ConsoleUI _consoleUI;
        public ViewerManager()
        {
            _accountService = new AccountService();
            _consoleUI = new ConsoleUI();
        }
        public void Start()
        {
            while (true)
            {
                _consoleUI.Head("WELLCOME TO THE BANK");
                int option = -1;
                try
                {
                    option = _consoleUI.SetAndSelectionEnumOption<EntryMenuOptions, string>(GetEntryMenuOptionDictionary());
                }
                catch (FormatException ex)
                {
                    string message = $"{ex.Message} \nThe option must be a valid integer";
                    _consoleUI.SpecialMessage(message);
                }
                switch ((EntryMenuOptions)option)
                {
                    case EntryMenuOptions.Leave:
                        return;
                    case EntryMenuOptions.CreateNewAccont:
                        this.CreateAccount();
                        break;
                    case EntryMenuOptions.AccessAccount:
                        this.AccessAccount();
                        break;
                    default:
                        string message = "Select one of the avaliable aoption!!";
                        _consoleUI.SpecialMessage(message);
                        break;
                }
            }
        }
        internal Dictionary<EntryMenuOptions, string> GetEntryMenuOptionDictionary()
        {
            return new Dictionary<EntryMenuOptions, string>()
            {
                {EntryMenuOptions.CreateNewAccont, "Create New Account" },
                {EntryMenuOptions.AccessAccount, "Access Account" },
                {EntryMenuOptions.Leave, "Leave" }
            };
        }
        internal void CreateAccount()
        {
            string name;
            decimal initialBalance = 0;

            _consoleUI.Head("CREATE NEW ACCOUNT");
            name = _consoleUI.GetString("Enter the client name: ");
            try
            {
                initialBalance = _consoleUI.GetDecimal("Enter the initial account balance: ");
            }
            catch (FormatException ex)
            {
                _consoleUI.SpecialMessage(ex.Message);
                return;
            }
            catch(OverflowException ex)
            {
                _consoleUI.SpecialMessage(ex.Message);
                return;
            }
            catch(Exception ex)
            {
                _consoleUI.SpecialMessage(ex.Message);
                return;
            }
 
            _accountService.CreateAccount(name, initialBalance);

            _consoleUI.SpecialMessage("Account creation Successfully complited", ConsoleColor.Green);
        }
        internal void AccessAccount()
        {

            string name;
            int accountId = 0;

            _consoleUI.Head("ACCESS ACCOUNT");
            name = _consoleUI.GetString("Enter the client name: ");
            try
            {
                accountId = _consoleUI.GetInt("Enter the account Id: ");
            }
            catch (FormatException ex)
            {
                _consoleUI.SpecialMessage(ex.Message);
                return;
            }
            catch(OverflowException ex)
            {
                _consoleUI.SpecialMessage(ex.Message);
                return;
            }
            catch(Exception ex)
            {
                _consoleUI.SpecialMessage(ex.Message);
                return;
            }

            ShowAccountData(name, accountId);

        }
        internal void ShowAccountData(string name, int accountId)
        {
            Console.Clear();
            ArrayList data = _accountService.GetAccountData(name, accountId);

            _consoleUI.Head("ACCOUNT DATA");
            _consoleUI.ShowAccountData(data);
        }
    }
}
