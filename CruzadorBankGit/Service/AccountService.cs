using CruzadorBankGit.Entity;
using CruzadorBankGit.Repository;
using Konscious.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CruzadorBankGit.Service
{
    internal class AccountService : IAccountService
    {
        private readonly AccountRepository _accountRepository;
        private readonly PasswordService _passwordService;

        public AccountService()
        {
            _accountRepository = new AccountRepository();
            _passwordService = new PasswordService();
        }
        public int CreateAccount(string name, decimal balance, string password, string passwordConfirmation)
        {
            if(balance < 0) throw new ArgumentOutOfRangeException(nameof(balance), "Balance should be equals ou bigger than 0");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name), "Name should be a valid, not null, empty or white Space message");
            if(password != passwordConfirmation) throw new ArgumentNullException(nameof(passwordConfirmation), "Both password should be equals");
            _passwordService.PasswordContentVerifier(password);

            byte[] salt = RandomNumberGenerator.GetBytes(16);

            byte[] HashedPassword = _passwordService.PasswordHasher(password, salt);

            int currentId = _accountRepository.GetCurrentId();
            int newId = ++currentId;

            Account account = new Account(newId, name, balance, HashedPassword, salt);

            _accountRepository.SetNewId(newId);

            _accountRepository.SaveNewAccount(account);

            return account.AccountId;
        }
        public IAccountSessionService Login(int accountId, string password)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password), "Password should not be null");
            if (accountId <= 0) throw new ArgumentOutOfRangeException(nameof(accountId), "AccountId should be a valid Integer bigger than 0");

            Account account = _accountRepository.GetAccount(accountId); 

            if (!_passwordService.PasswordVerify(password, account.Password, account.Salt)) throw new Exception(); //Criar PasswordException

            return new AccountSessionService(account);
        }
    }
}
