using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.Entity
{
    internal class Account
    {
        private int _accountId;
        public int AccountId
        {
            get
            {
                return _accountId;
            }
            init
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(AccountId),"The value should be bigger then 0"); // especificar depois
                _accountId = value;
            }
        }
        public string Name { get; internal set; }
        private decimal _balance;
        public decimal Balance
        {
            get
            {
                return _balance;
            }
            internal set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Balance), "The value shouldn't be lower then 0"); // especificar depois
                _balance = value;
            }
        }
        public byte[] Password { get; internal set; }
        public byte[] Salt { get; internal set; }
        public Account(int accountId, string name, decimal balance, byte[] password, byte[] salt)
        {
            AccountId = accountId;
            Name = name;
            Balance = balance;
            Password = password;
            Salt = salt;
        }
        public bool Withdrawal(decimal amount)
        {
            if (amount <= 0) return false;
            if (amount >= Balance) return false;
            Balance -= amount;
            return true;
        }
        public bool Deposit(decimal amount)
        {
            if(amount <= 0) return false;
            Balance += amount;
            return true;
        }
        public void ChangePassword(byte[] password, byte[] salt)
        {
            // Optei por deixar a validacao no service
            Password = password;
            Salt = salt;
        }
    }
}
