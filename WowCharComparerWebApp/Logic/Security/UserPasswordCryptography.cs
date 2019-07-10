﻿using System;
using System.Security.Cryptography;
using WowCharComparerWebApp.Data.Database.Repository.Users;
using WowCharComparerWebApp.Models.PasswordEncryption;

namespace WowCharComparerWebApp.Logic.Security
{
    public class UserPasswordCryptography : IDisposable
    {
        private string _password = string.Empty;

        /// <summary>
        /// Constructor. Initialize new UserPasswordCryptography class
        /// </summary>
        /// <param name="password">Plain user's password</param>
        public UserPasswordCryptography(string password)
        {
            _password = password;
        }

        /// <summary>
        /// Encrypt user password using salt and hashing algorythm
        /// </summary>
        /// <returns>Password encryption result</returns>
        internal PasswordEncryptionResult EncryptUserPassword()
        {
            byte[] generatedSalt = GenerateRandomSalt();
            string hashedPassword = HashPassword(_password, generatedSalt);

            return new PasswordEncryptionResult(generatedSalt, hashedPassword);
        }

        /// <summary>
        /// Decrypt and authenticate existing user's password
        /// </summary>
        /// <param name="user">Existing user</param>
        /// <returns>Authenticate state</returns>
        internal bool AuthenticateUserPassword(Models.Internal.User user)
        {
            string hashedPassword = HashPassword(_password, System.Text.Encoding.UTF8.GetBytes(user.Salt));

            return hashedPassword.Equals(user.HashedPassword);
        }

        /// <summary>
        /// Hash password using Rfc2898DeriveBytes algorithm
        /// </summary>
        /// <param name="password"></param>
        /// <param name="saltSize"></param>
        /// <param name="keySize"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        private string HashPassword(string plainPassword, byte[] salt, int keySize = 32, int iterations = 1000)
        {
            string hashedPassword = string.Empty;

            using (var algorithm = new Rfc2898DeriveBytes(plainPassword, salt, iterations, HashAlgorithmName.SHA256))
            {
                hashedPassword = Convert.ToBase64String(algorithm.GetBytes(keySize));
            }

            return hashedPassword;
        }

        /// <summary>
        /// Generates random sequence of bytes to salt password
        /// </summary>
        /// <returns></returns>
        private byte[] GenerateRandomSalt()
        {
            byte[] saltArray = new byte[32];

            using (var rNGCryptoProvider = new RNGCryptoServiceProvider())
            {
                rNGCryptoProvider.GetBytes(saltArray);
            }

            return saltArray;
        }

        public void Dispose()
        {
            _password = null;
            GC.SuppressFinalize(this);
        }
    }
}