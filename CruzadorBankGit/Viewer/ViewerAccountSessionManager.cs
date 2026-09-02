using CruzadorBankGit.DataTransferObject;
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
            _consoleUI.SpecialMessage("Login concluded with succes ...", ConsoleColor.Green);

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
                    string message = $"{ex.Message} \nThe option must be a valid integer";
                    _consoleUI.SpecialMessage(message);
                }
                switch ((ViewerSessionOptions)option)
                {
                    case ViewerSessionOptions.Leave:
                        return;
                    case ViewerSessionOptions.Withdrawal:
                        Console.Clear();
                        Console.WriteLine("Adicionar Withdrawal");
                        Console.ReadKey();
                        break;
                    case ViewerSessionOptions.Deposit:
                        Console.Clear();
                        Console.WriteLine("Adicionar Deposit");
                        Console.ReadKey();
                        break;
                    default:
                        string message = "Select one of the avaliable aoption!!";
                        _consoleUI.SpecialMessage(message);
                        break;
                }
            }
        }
        public Dictionary<ViewerSessionOptions, string> GetViewerSessionOptionDictionary()
        {
            return new Dictionary<ViewerSessionOptions, string>
            {
                {ViewerSessionOptions.Leave, "Leave"},
                {ViewerSessionOptions.Withdrawal, "Make a withdawal"},
                {ViewerSessionOptions.Deposit, "Make a deposit"}
            };
        }
    }
}
