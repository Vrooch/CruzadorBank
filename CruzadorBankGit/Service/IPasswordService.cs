using System;
using System.Collections.Generic;
using System.Text;

namespace CruzadorBankGit.Service
{
    internal interface IPasswordService
    {
        public byte[] PasswordHasher(string password, byte[] salt);
        public bool PasswordVerify(string password, byte[] currentPassword, byte[] salt);
        public void PasswordContentVerifier(string password); // void pq a verificacao deve acontecer com base no lancamento de excecoes
    }
}
