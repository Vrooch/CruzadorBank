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
            _consoleUI.SpecialMessage("logado ...", ConsoleColor.Green);
        }
    }
}
