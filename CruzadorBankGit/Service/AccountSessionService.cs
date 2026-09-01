using CruzadorBankGit.DataTransferObject;
using CruzadorBankGit.Entity;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace CruzadorBankGit.Service
{
    internal class AccountSessionService : IAccountSessionService
    {
        private readonly Account _account;
        private readonly PasswordService _passwordService;
        public AccountSessionService(Account account)
        {
            _account = account;
            _passwordService = new PasswordService();
        }
        void IAccountSessionService.Deposit(decimal amount)
        {
            throw new NotImplementedException();
        }
        AccountDTO IAccountSessionService.GetAccountData()
        {
            throw new NotImplementedException();
        }
        void IAccountSessionService.Withdrawal(decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
