using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.ResultObjects
{
    internal class ValidationResult : IValidationResult
    {
        public bool Result { get; private set; }

        private readonly List<string> _messagens;

        public ValidationResult()
        {
            Result = true;
            _messagens = new List<string>();
        }

        public void ThrowError(string message)
        {
            Result = false;
            _messagens.Add(message);
        }
        public string GetMessage()
        {
            return string.Join("\n", _messagens);
        }
    }
}
