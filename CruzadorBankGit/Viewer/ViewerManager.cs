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
        private readonly IAccountService _accountService;
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
                        this.Login();
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
            while (true)
            {
                string name;
                decimal initialBalance = 0;
                string password;
                string passwordConfrimation;

                _consoleUI.Head("CREATE NEW ACCOUNT");
                name = _consoleUI.GetString("Enter the client name: ");
                try
                {
                    initialBalance = _consoleUI.GetDecimal("Enter the initial account balance: ");
                }
                catch (FormatException ex)
                {
                    _consoleUI.SpecialMessage("The Initial balance should be a valid decimal number");
                    continue;
                }
                catch (OverflowException ex)
                {
                    _consoleUI.SpecialMessage($"The initinal number should be a positive equals or bigger than 0, and lower then {decimal.MaxValue}");
                    continue;
                }
                catch (Exception ex)
                {
                    _consoleUI.SpecialMessage($"Not defined error: \n{ex.Message}");
                    continue;
                }

                password = _consoleUI.GetString("Enter the password: ");
                passwordConfrimation = _consoleUI.GetString("Confirme the password: ");

                int accountId = _accountService.CreateAccount(name, initialBalance, password, passwordConfrimation);

                _consoleUI.SpecialMessage($"Account creation Successfully complited\nNew Account ID: {accountId}", ConsoleColor.Green);
                break;
            }
        }
        internal void Login()
        {
            IAccountSessionService accountSessionService;

            while (true)
            {
                int accountId = 0;

                _consoleUI.Head("ACCESS ACCOUNT");
                try
                {
                    accountId = _consoleUI.GetInt("Enter the account Id: ");
                }
                catch (FormatException ex)
                {
                    _consoleUI.SpecialMessage("The account ID should be a valid integer number");
                    continue;
                }
                catch (OverflowException ex)
                {
                    _consoleUI.SpecialMessage($"The account ID should be a positive equals or bigger than 0, and lower then {int.MaxValue}");
                    continue;
                }
                catch (Exception ex)
                {
                    _consoleUI.SpecialMessage(ex.Message);
                    continue;
                }

                string password = _consoleUI.GetString("Enter the password: ");

                accountSessionService = _accountService.Login(accountId, password);
                break;
            }

            ViewerAccountSessionManager viewerAccountSessionManager = new ViewerAccountSessionManager(accountSessionService);

            viewerAccountSessionManager.start();

        }

    }
}
