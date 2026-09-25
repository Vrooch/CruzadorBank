using CruzadorBankGit.Entity;
using CruzadorBankGit.Exceptions.Account;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Text.Json;

namespace CruzadorBankGit.Repository
{
    internal class AccountRepository
    {
        private string _idCouterPash = @"C:\Projetos_Csharp\Portifolio\CruzadorBankGit\CruzadorBankGit\Repository\Ids\IdCounter.json";
        private string _accountsDirectioryPath = @"C:\Projetos_Csharp\Portifolio\CruzadorBankGit\CruzadorBankGit\Repository\Accounts\";
        public AccountRepository()
        {
            if (!Path.Exists(_idCouterPash)) IdCouterInitializator();
        }
        public void IdCouterInitializator()
        {
            using (FileStream stream = new FileStream(_idCouterPash, FileMode.Create))
            {
                JsonSerializer.Serialize(stream, 0);
            }
        }
        public int GetCurrentId()
        {
                int id;
                using (FileStream stream = new FileStream(_idCouterPash, FileMode.Open))
                {
                    id = JsonSerializer.Deserialize<int>(stream);
                }
                return id;
        }
        public void SetNewId(int newId)
        {
            using (FileStream stream = new FileStream(_idCouterPash, FileMode.Create))
            {
                JsonSerializer.Serialize(stream, newId); 
            }
        }
        public void SaveNewAccount(Account account)
        {
            if(account is null) throw new AccountException( "Account shouldnt be a null object");

            string accountPath = Path.Combine(_accountsDirectioryPath, $"{account.AccountId.ToString()}");
            accountPath += ".json";

            if (File.Exists(accountPath)) throw new AccountException("Informed Account already exists");

            using (FileStream stream = new FileStream(accountPath, FileMode.Create))
            {
                JsonSerializer.Serialize(stream, account);
            }
        }
        public void SaveAccount(Account account)
        {
            if (account is null) throw new AccountException("Account shouldnt be a null object");

            string accountPath = Path.Combine(_accountsDirectioryPath, $"{account.AccountId.ToString()}");
            accountPath += ".json";

            if (!File.Exists(accountPath)) throw new AccountException("Informed Account doest not exists");

            using (FileStream stream = new FileStream(accountPath, FileMode.Truncate))
            {
                JsonSerializer.Serialize(stream, account);
            }
        }
        public Account GetAccount(int accountId)
        {
            string accountPath = Path.Combine(_accountsDirectioryPath, $"{accountId}");
            accountPath += ".json";

            if (!File.Exists(accountPath)) throw new AccountException("Informed Account does not exists");

            Account account;
            using (FileStream stream = new FileStream(accountPath, FileMode.Open))
            {
                account = JsonSerializer.Deserialize<Account>(stream);
            }
            return account;
        }
    }
}
