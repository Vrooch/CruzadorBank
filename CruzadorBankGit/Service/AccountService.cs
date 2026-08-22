using CruzadorBankGit.Repository;
using CruzadorBankGit.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CruzadorBankGit.Service
{
    internal class AccountService
    {
        private readonly AccountRepository _accountRepository;

        public AccountService()
        {
            _accountRepository = new AccountRepository();
        }
        public void CreateAccount(string name, decimal balance)
        {
            if(balance < 0) throw new ArgumentOutOfRangeException(nameof(balance), "Balance should be equals ou bigger than 0");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name), "Name should be a valid, not null, empty or white Space message");

            int currentId = _accountRepository.GetCurrentId();
            int newId = ++currentId;

            Account account = new Account(newId, name, balance);

            _accountRepository.SetNewId(newId);

            _accountRepository.SaveNewAccount(account);
        }
        public Account GetAccount(string name,  int accountId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name), "Name should be a valid, not null, empty or white Space message");
            if (accountId <= 0) throw new ArgumentOutOfRangeException(nameof(accountId), "Value Informed is not a valid Accont Id");

            return _accountRepository.GetAccount(name, accountId);
        }
        public ArrayList GetAccountData(string name, int accountId)
        {
            Account account = GetAccount(name, accountId);
            return new ArrayList { account.AccountId, account.Name, account.Balance };
        }
    }
}
