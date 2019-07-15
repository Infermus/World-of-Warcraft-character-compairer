namespace WowCharComparerWebApp.Models.PasswordEncryption
{
    internal class PasswordEncryptionResult
    {
        public string Salt { get; private set; }

        public string HashedPassword { get; private set; }

        public PasswordEncryptionResult(string salt, string hashedPassword)
        {
            Salt = salt;
            HashedPassword = hashedPassword;
        }
    }
}
