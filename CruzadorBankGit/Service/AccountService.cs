using CruzadorBankGit.Entity;
using CruzadorBankGit.Exceptions.Account;
using CruzadorBankGit.Exceptions.Password;
using CruzadorBankGit.Repository;
using CruzadorBankGit.ResultObjects;
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
            if (balance < 0) throw new FinancialAmountException("Balance should be equals ou bigger than 0");
            
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(null, "Name should be a valid, not null, empty or white Space message");
            if (name[name.Length - 1] == ' ') name = name[..^1]; // remover " "
            if (!name.Split(' ').All(x => x[0].ToString() == x[0].ToString().ToUpper())) throw new ArgumentException("A inicial de cada nome deve estar em caixa alta");
            if (!name.Split(' ').All(x => x[1..] == x[1..].ToLower())) throw new ArgumentException("Somente a inicial de cada nome deve estar em caixa alta");

            if (password != passwordConfirmation) throw new PasswordContentException("Both password should be equals");

            IValidationResult passwordContantValidation = _passwordService.PasswordContentVerifier(password);
            if (!passwordContantValidation.Result) throw new PasswordContentException(passwordContantValidation.GetMessage());

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
            if (string.IsNullOrEmpty(password)) throw new PasswordException("Password should not be null");
            if (accountId <= 0) throw new AccountException( "AccountId should be a valid Integer bigger than 0");

            Account account = _accountRepository.GetAccount(accountId); 

            if (!_passwordService.PasswordVerify(password, account.Password, account.Salt)) throw new PasswordException("Wrong Password informed");

            return new AccountSessionService(account);
        }
    }
}
