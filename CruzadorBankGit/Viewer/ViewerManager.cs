using CruzadorBankGit.Entity;
using CruzadorBankGit.Exceptions.Account;
using CruzadorBankGit.Exceptions.Password;
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
                    _consoleUI.SpecialMessage("The option must be an INTEGER, that curresponds to a valid option", clear: false, timer: true);
                    continue;
                }
                catch (OverflowException ex)
                {
                    _consoleUI.SpecialMessage("The option must be an INTEGER, that curresponds to a valid option", clear: false, timer: true);
                    continue;
                }
                catch (Exception ex)
                {
                    _consoleUI.SpecialMessage($"Unexpected Error: \n{ex.Message}\n{ex.StackTrace}", clear: false, timer: true);
                    continue;
                }

                switch ((EntryMenuOptions)option)
                {
                    case EntryMenuOptions.Leave:
                        return;
                    case EntryMenuOptions.CreateNewAccont:
                        this.CreateAccount();
                        break;
                    case EntryMenuOptions.Login:
                        this.Login();
                        break;
                    default:
                        _consoleUI.SpecialMessage("Select one of the avaliable aoption!!", clear: false, timer: true);
                        break;
                }
            }
        }
        internal Dictionary<EntryMenuOptions, string> GetEntryMenuOptionDictionary()
        {
            return new Dictionary<EntryMenuOptions, string>()
            {
                {EntryMenuOptions.CreateNewAccont, "Create New Account" },
                {EntryMenuOptions.Login, "Account Login" },
                {EntryMenuOptions.Leave, "Leave" }
            };
        }
        internal void CreateAccount()
        {
            int attemptsAmount = 0;
            while (true)
            {
                if (attemptsAmount == 3)
                {
                    _consoleUI.SpecialMessage("Maximum attempts amount reached", clear: false, timer: true);
                    return;
                }
                attemptsAmount++;

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
                    _consoleUI.SpecialMessage("The Initial balance should be a valid decimal number", clear: false, timer: true);
                    continue;
                }
                catch (OverflowException ex)
                {
                    _consoleUI.SpecialMessage($"The initinal number should be a positive equals or bigger than 0, and lower then {decimal.MaxValue}", clear: false, timer: true);
                    continue;
                }
                catch (Exception ex)
                {
                    _consoleUI.SpecialMessage($"Unexpected Error: \n{ex.Message}\n{ex.StackTrace}", clear: false, timer: true);
                    continue;
                }

                password = _consoleUI.GetString("Enter the password: ");
                passwordConfrimation = _consoleUI.GetString("Confirme the password: ");

                int accountId = 0;

                try
                {
                    accountId = _accountService.CreateAccount(name, initialBalance, password, passwordConfrimation);
                }
                catch (PasswordContentException ex)
                {
                    _consoleUI.SpecialMessage(ex.Message, clear: false, timer: true);
                    continue;
                }
                catch (AccountException ex)
                {
                    _consoleUI.SpecialMessage(ex.Message, clear: false, timer: true);
                    continue;
                }
                catch (FormatException ex)
                {
                    _consoleUI.SpecialMessage(ex.Message, clear: false, timer: true);
                    continue;
                }
                catch (FinancialAmountException ex)
                {
                    _consoleUI.SpecialMessage(ex.Message, clear: false, timer: true);
                    continue;
                }
                catch (ArgumentNullException ex)
                {
                    _consoleUI.SpecialMessage(ex.Message, clear: false, timer: true);
                    continue;
                }
                catch (ArgumentException ex)
                {
                    _consoleUI.SpecialMessage(ex.Message, clear: false, timer: true);
                    continue;
                }
                catch (Exception ex)
                {
                    _consoleUI.SpecialMessage($"Unexpected Error: \n{ex.Message}\n{ex.StackTrace}", clear: false, timer: true);
                    continue;
                }

                _consoleUI.SpecialMessage($"Account creation Successfully complited\nNew Account ID: {accountId}", ConsoleColor.Green, false, true,  4000);
                break;
            }
        }
        internal void Login()
        {
            IAccountSessionService accountSessionService;

            int attemptsAmount = 0;
            while (true)
            {
                if (attemptsAmount == 3)
                {
                    _consoleUI.SpecialMessage("Maximum attempts amount reached", clear: false, timer: true);
                    return;
                }
                attemptsAmount++;

                int accountId = 0;

                _consoleUI.Head("ACCESS ACCOUNT");
                try
                {
                    accountId = _consoleUI.GetInt("Enter the account Id: ");
                }
                catch (FormatException ex)
                {
                    _consoleUI.SpecialMessage("The account ID should be a valid integer number", clear: false, timer: true);
                    continue;
                }
                catch (OverflowException ex)
                {
                    _consoleUI.SpecialMessage($"The account ID should be a positive equals or bigger than 0, and lower then {int.MaxValue}", clear: false, timer: true);
                    continue;
                }
                catch (Exception ex)
                {
                    _consoleUI.SpecialMessage($"Unexpected Error: \n{ex.Message}\n{ex.StackTrace}", clear: false, timer: true);
                    continue;
                }

                string password = _consoleUI.GetString("Enter the password: ");

                try
                {
                    accountSessionService = _accountService.Login(accountId, password);
                    break;
                }
                catch(PasswordException ex)
                {
                    _consoleUI.SpecialMessage(ex.Message, clear: false, timer: true);
                    continue;
                }
                catch (AccountException ex)
                {
                    _consoleUI.SpecialMessage(ex.Message, clear: false, timer: true);
                    continue;
                }
                catch (Exception ex)
                {
                    _consoleUI.SpecialMessage($"Unexpected Error: \n{ex.Message}\n{ex.StackTrace}", clear: false, timer: true);
                    continue;
                }
            }

            ViewerAccountSessionManager viewerAccountSessionManager = new ViewerAccountSessionManager(accountSessionService);

            viewerAccountSessionManager.start();

        }

    }
}
