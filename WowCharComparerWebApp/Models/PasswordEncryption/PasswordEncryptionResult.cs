namespace WowCharComparerWebApp.Models.PasswordEncryption
{
    internal class PasswordEncryptionResult
    {
        public byte[] Salt { get; private set; }

        public string HashedPassword { get; private set; }

        public PasswordEncryptionResult(byte[] salt, string hashedPassword)
        {
            Salt = salt;
            HashedPassword = hashedPassword;
        }

        public string GetSaltAsString()
        {
            return Salt.ToString();
        }    
    }
}
