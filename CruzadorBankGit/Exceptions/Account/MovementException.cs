using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.Exceptions.Account
{
    internal class MovementException : Exception
    {
        public MovementException() { }
        public MovementException(string message) : base(message) { }
        public MovementException(string message, Exception? innerException) : base(message, innerException) { }
    }
}
