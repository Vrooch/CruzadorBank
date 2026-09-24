using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.Exceptions.Account
{
    internal class ZeroBalanceException : Exception
    {
        public ZeroBalanceException() { }
        public ZeroBalanceException(string message) : base(message) { }
        public ZeroBalanceException(string message, Exception? innerException) : base(message, innerException) { }
    }
}
