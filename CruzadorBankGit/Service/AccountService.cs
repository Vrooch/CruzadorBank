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
    internal class AccountService : IPasswordService
    {
        private readonly AccountRepository _accountRepository;
        public int DegreeOfParallelism = 4; // quantidade de trabalho paralelo
        public int MemorySize = 65536; // Custo de memoria --> aprox 64mb
        public int Iterations = 3; // Numero de ciclos

        public AccountService()
        {
            _accountRepository = new AccountRepository();
        }
        public int CreateAccount(string name, decimal balance, string password, string passwordConfirmation)
        {
            if(balance < 0) throw new ArgumentOutOfRangeException(nameof(balance), "Balance should be equals ou bigger than 0");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name), "Name should be a valid, not null, empty or white Space message");
            if(password != passwordConfirmation) throw new ArgumentNullException(nameof(passwordConfirmation), "Both password should be equals");
            PasswordContentVerifier(password);

            byte[] salt = RandomNumberGenerator.GetBytes(16);

            byte[] HashedPassword = PasswordHasher(password, salt);

            int currentId = _accountRepository.GetCurrentId();
            int newId = ++currentId;

            Account account = new Account(newId, name, balance, HashedPassword, salt);

            _accountRepository.SetNewId(newId);

            _accountRepository.SaveNewAccount(account);

            return account.AccountId;
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

        public void PasswordContentVerifier(string password)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password), "Password should not be null or empty");
            if (password.Length <= 5) throw new ArgumentOutOfRangeException(nameof(password), "The password should be bigger than 5 character");
            if (!password.Any(char.IsUpper)) throw new ArgumentException(nameof(password), "Password should have at least one uppert case letter");
            //adicionar novas regras de composicao de senhas
        }

        public byte[] PasswordHasher(string password, byte[] salt)
        {
            using (Argon2id argon = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                argon.Salt = salt;
                argon.DegreeOfParallelism = this.DegreeOfParallelism;
                argon.MemorySize = this.MemorySize;
                argon.Iterations = this.Iterations;

                return argon.GetBytes(32);
            }
        }

        public bool PasswordVerify(string password, byte[] currentPassword, byte[] salt)
        {
            byte[] HashedPassword = PasswordHasher(password, salt);
            return CryptographicOperations.FixedTimeEquals(HashedPassword, currentPassword);
        }
    }
}
