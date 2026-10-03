using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.Entity
{
    internal interface IAccountTransferable
    {
        public void ReceiveTransfer(decimal amount);
    }
}
