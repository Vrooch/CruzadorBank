using CruzadorBankGit.DataTransferObject;
using CruzadorBankGit.Entity;
using CruzadorBankGit.Repository;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace CruzadorBankGit.Service
{
    internal class AccountSessionService : IAccountSessionService
    {
        private readonly Account _account;
        private readonly AccountRepository _accountRepository;
        public AccountSessionService(Account account)
        {
            _account = account;
            _accountRepository = new AccountRepository();
        }
        public void Deposit(decimal amount)
        {
            _account.Deposit(amount);
            _accountRepository.SaveAccount(this._account);
        }
        public AccountDTO GetAccountData()
        {
            return new AccountDTO(_account.AccountId, _account.Name, _account.Balance);
        }
        public void Withdrawal(decimal amount)
        {
            _account.Withdrawal(amount);
            _accountRepository.SaveAccount(this._account);
        }
        public void SaveAccount()
        {
            _accountRepository.SaveAccount(this._account);
        }
    }
}
