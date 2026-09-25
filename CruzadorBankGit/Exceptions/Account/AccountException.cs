using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.Exceptions.Account
{
    internal class AccountException : Exception
    {
        public AccountException() { }
        public AccountException(string message) : base(message) { }
        public AccountException(string message, Exception? innerException) : base(message, innerException) { }
    }
}
