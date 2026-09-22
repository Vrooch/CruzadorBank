using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.Exceptions.Password
{
    internal class PasswordException : Exception
    {
        public PasswordException() : base() { }
        public PasswordException(string message) : base(message) { }
        public PasswordException(string message, Exception? innerException) : base(message, innerException) { }
    }
}
