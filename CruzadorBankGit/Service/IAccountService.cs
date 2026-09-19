using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.Service
{
    internal interface IAccountService
    {
        public int CreateAccount(string name, decimal balance, string password, string passwordConfirmation);
        public IAccountSessionService Login(int accountId, string password);
    }
}
