using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.Exceptions.Password
{
    internal class PasswordContentException : Exception 
    {
        public PasswordContentException() { }
        public PasswordContentException(string message) : base(message) { }
        public PasswordContentException(string messahe, Exception? innerException) : base(messahe, innerException) { }
    }
}
