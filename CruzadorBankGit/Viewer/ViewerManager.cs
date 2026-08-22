using CruzadorBankGit.Entity;
using CruzadorBankGit.Service;
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
            _consoleUI.Head("CREATE NEW ACCOUNT");
            string name = _consoleUI.GetString("Enter the client name: ");
            decimal initialBalance = _consoleUI.GetDecimal("Enter the initial account balance: ");
 
            _accountService.CreateAccount(name, initialBalance);

            _consoleUI.SpecialMessage("Account creation Successfully complited", ConsoleColor.Green);
        }
        internal void AccessAccount()
        {

            _consoleUI.Head("ACCESS ACCOUNT");
            string name = _consoleUI.GetString("Enter the client name: ");
            int accountId = _consoleUI.GetInt("Enter the account Id: ");

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
