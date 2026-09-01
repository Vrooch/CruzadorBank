using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.DataTransferObject
{
    // DTO: data transfer object -> garanti que o viewer nao conheca Account e realizar a transferencia de dados da conta do service para o viewer
    internal class AccountDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public decimal Balance { get; init; }

        public AccountDTO (int id, string name, decimal balance)
        {
            Id = id;
            Name = name;
            Balance = balance;
        }
    }
}
