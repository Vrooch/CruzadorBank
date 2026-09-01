using CruzadorBankGit.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.Service
{
    internal interface IAccountSessionService
    {
        public void Withdrawal(decimal amount);
        public void Deposit(decimal amount);
        public AccountDTO GetAccountData();
    }
}
