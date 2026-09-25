using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.Exceptions.Account
{
    internal class FinancialAmountException : Exception
    {
        public FinancialAmountException() { }
        public FinancialAmountException(string message) : base(message) { }
        public FinancialAmountException(string message, Exception? innerException) : base(message, innerException) { }
    }
}
