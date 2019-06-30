using System;
using System.Security.Cryptography;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Models.Internal;

namespace WowCharComparerWebApp.Logic.Security
{
    public class UserPasswordCryptography
    {
        internal string EncryptUserPassword(string password)
        {
            string hashedPassword = HashPassword(password);

            throw new NotImplementedException();
        }

        internal string DecryptUserPassword(Models.Internal.User user)
        {
            string hashedPassword = HashPassword(user.Password);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Hash password using Rfc2898DeriveBytes algorithm
        /// </summary>
        /// <param name="password"></param>
        /// <param name="saltSize"></param>
        /// <param name="keySize"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        private string HashPassword(string plainPassword, int saltSize = 16, int keySize = 32, int iterations = 1000)
        {
            using (var algorithm = new Rfc2898DeriveBytes(plainPassword, saltSize, iterations, HashAlgorithmName.SHA256))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{salt}{key}";
            }
        }
    }
}
