using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.Entity
{
    internal interface IAccountTransferable
    {
        public int AccountId { get; }
        public void ReceiveTransfer(decimal amount);
    }
}
