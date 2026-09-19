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
        private readonly PasswordService _passwordService; // ver de remover
        private readonly AccountRepository _accountRepository;
        public AccountSessionService(Account account)
        {
            _account = account;
            _passwordService = new PasswordService();
            _accountRepository = new AccountRepository();
        }
        public void Deposit(decimal amount)
        {
            throw new NotImplementedException();
            /*
             * 1. Chamar o metodo responsavel por realizar a operacao
             * 2. Salvar as alteracoes
             */
        }
        public AccountDTO GetAccountData()
        {
            return new AccountDTO(_account.AccountId, _account.Name, _account.Balance);
        }
        public void Withdrawal(decimal amount)
        {
            /*
             * 1. Chamar o metodo responsavel por realizar a operacao
             * 2. Salvar as alteracoes
             */
            _account.Withdrawal(amount);
            _accountRepository.SaveAccount(this._account);

        }
    }
}
