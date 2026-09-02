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
        public void Deposit(decimal amount)
        {
            throw new NotImplementedException();
        }
        public AccountDTO GetAccountData()
        {
            return new AccountDTO(_account.AccountId, _account.Name, _account.Balance);
        }
        public void Withdrawal(decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
