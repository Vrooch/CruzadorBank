using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.ResultObjects
{
    internal interface IValidationResult
    {
        public bool Result { get;}
        public string GetMessage();
    }
}
