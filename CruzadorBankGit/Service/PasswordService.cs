using CruzadorBankGit.Exceptions.Password;
using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CruzadorBankGit.Service
{
    internal class PasswordService
    {
        public int DegreeOfParallelism = 4; // quantidade de trabalho paralelo
        public int MemorySize = 65536; // Custo de memoria --> aprox 64mb
        public int Iterations = 3; // Numero de ciclos

        public void PasswordContentVerifier(string password)
        {
            if (string.IsNullOrEmpty(password)) throw new PasswordContentException("Password should not be null or empty");
            if (password.Length <= 5) throw new PasswordContentException("The password should be bigger than 5 character");
            if (!password.Any(char.IsUpper)) throw new PasswordContentException("Password should have at least one upper case letter");
            if (!password.Any(char.IsLower)) throw new PasswordContentException("Password should have at least one lower case letter");
        }
        public byte[] PasswordHasher(string password, byte[] salt)
        {
            using (Argon2id argon = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                argon.Salt = salt;
                argon.DegreeOfParallelism = this.DegreeOfParallelism;
                argon.MemorySize = this.MemorySize;
                argon.Iterations = this.Iterations;

                return argon.GetBytes(32);
            }
        }
        public bool PasswordVerify(string password, byte[] currentPassword, byte[] salt)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(null, "Password should not be null or empty");
            byte[] HashedPassword = PasswordHasher(password, salt);
            return CryptographicOperations.FixedTimeEquals(HashedPassword, currentPassword);
        }
    }
}
