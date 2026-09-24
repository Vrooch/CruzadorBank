using CruzadorBankGit.DataTransferObject;
using CruzadorBankGit.Exceptions.Account;
using CruzadorBankGit.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.Viewer
{
    /// <summary>
    /// To orchestrate the viewer layer during the user session in user account
    /// </summary>
    internal class ViewerAccountSessionManager
    {
        private readonly IAccountSessionService _accountSessionService;
        private readonly ConsoleUI _consoleUI;

        public ViewerAccountSessionManager(IAccountSessionService accountSessionService)
        {
            _accountSessionService = accountSessionService;
            _consoleUI = new ConsoleUI();
        }
        public void start()
        {
            _consoleUI.SpecialMessage("Login concluded with succes ...", ConsoleColor.Green, timer: true, time:1500);

            while (true)
            {
                AccountDTO accountDTO = _accountSessionService.GetAccountData();
                _consoleUI.Head($"{accountDTO.Name} | {accountDTO.Id}");
                _consoleUI.ShowBalance(accountDTO.Balance);

                int option = -1;
                try
                {
                    option = _consoleUI.SetAndSelectionEnumOption<ViewerSessionOptions, string>(GetViewerSessionOptionDictionary());
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

                switch ((ViewerSessionOptions)option)
                {
                    case ViewerSessionOptions.Leave:
                        _accountSessionService.SaveAccount();
                        return;
                    case ViewerSessionOptions.Withdrawal:
                        Withdrawal();
                        break;
                    case ViewerSessionOptions.Deposit:
                        Deposit();
                        break;
                    default:
                        _consoleUI.SpecialMessage("Select one of the avaliable aoption!!", clear: false, timer: true);
                        break;
                }
            }
        }
        internal Dictionary<ViewerSessionOptions, string> GetViewerSessionOptionDictionary()
        {
            return new Dictionary<ViewerSessionOptions, string>
            {
                {ViewerSessionOptions.Withdrawal, "Make a withdawal"},
                {ViewerSessionOptions.Deposit, "Make a deposit"},
                {ViewerSessionOptions.Leave, "Leave"}
            };
        }

        internal void Withdrawal()
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

                decimal amount;

                try
                {
                    amount = _consoleUI.AccountMoviment("Withdrawal");
                }
                catch (FormatException ex)
                {
                    _consoleUI.SpecialMessage("The amount should be a valid decimal number", clear: false, timer: true);
                    continue;
                }
                catch (OverflowException ex)
                {
                    _consoleUI.SpecialMessage($"The amount should be a positive equals or bigger than 0, and lower then {decimal.MaxValue}", clear: false, timer: true);
                    continue;
                }
                catch (Exception ex)
                {
                    _consoleUI.SpecialMessage($"Unexpected Error: \n{ex.Message}\n{ex.StackTrace}", clear: false, timer: true);
                    continue;
                }

                try
                {
                    _accountSessionService.Withdrawal(amount);
                }
                catch (FinancialAmountException ex)
                {
                    _consoleUI.SpecialMessage(ex.Message, clear: false, timer: true);
                    continue;
                }
                catch (ZeroBalanceException ex)
                {
                    _consoleUI.SpecialMessage(ex.Message, clear: false, timer: true);
                    break;
                    // Nesse caso aplicamos breack porque se n sair da operacao, o user ficara preso em um loop de exception sem saida
                }
                catch (Exception ex)
                {
                    _consoleUI.SpecialMessage($"Unexpected Error: \n{ex.Message}\n{ex.StackTrace}", clear: false, timer: true);
                    continue;
                }

                _consoleUI.SpecialMessage("Process finished with success", ConsoleColor.Green, false, true, 2500);
                break;
            }
        }
        internal void Deposit()
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

                decimal amount;
                try
                {
                    amount = _consoleUI.AccountMoviment("Deposit");
                }
                catch (FormatException ex)
                {
                    _consoleUI.SpecialMessage("The amount should be a valid decimal number", clear: false, timer: true);
                    continue;
                }
                catch (OverflowException ex)
                {
                    _consoleUI.SpecialMessage($"The amount should be a positive equals or bigger than 0, and lower then {decimal.MaxValue}", clear: false, timer: true);
                    continue;
                }
                catch (Exception ex)
                {
                    _consoleUI.SpecialMessage($"Unexpected Error: \n{ex.Message}\n{ex.StackTrace}", clear: false, timer: true);
                    continue;
                }

                try
                {
                    _accountSessionService.Deposit(amount);
                }
                catch (FinancialAmountException ex)
                {
                    _consoleUI.SpecialMessage(ex.Message, clear: false, timer: true);
                    continue;
                }
                catch (Exception ex)
                {
                    _consoleUI.SpecialMessage($"Unexpected Error: \n{ex.Message}\n{ex.StackTrace}", clear: false, timer: true);
                    continue;
                }

                _consoleUI.SpecialMessage("Process finished with success", ConsoleColor.Green, false, true);
                break;
            }
        }

    }
}
