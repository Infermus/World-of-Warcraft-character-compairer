
namespace WowCharComparerWebApp.Configuration
{
    public static class UserMessages
    {
        public const string PasswordHasNoNumbers = "Password should contains at least one number";
        public const string PasswordHasNoCapitalLetter = "Password should contains at least one capital letter";
        public const string ConfirmPasswordNoMatch = "Passwords no match";
        public const string InvalidPassword = "Invalid password";
        public const string PasswordLenghtTooShort = "Password should be at least 8 characters long";
        public const string NameLengthTooShort = "User Name should be at least 6 characters long";
        public const string AlreadyExists = "User Name already exists";
        public const string EmailInvalidFormat = "Email address has invalid format";

        public const string UserNameIsRequired = "User Name is required";
        public const string PasswordIsRequired = "Password is required";
        public const string PasswordConfirmationIsRequired = "Password confirmation is required";
        public const string EmailIsRequired = "Email is required";

        public const string PasswordRecoveryEmailSend = "An email has been sent to you with a link which you may use to reset your password.";
        public const string PasswordChangeTimeout = "Time for changing password runned out. Please try again.";
        public const string PasswordHasBeenChanged = "Password has been changed.";

        #region User Message methods

        public static string LoginGreetings(string userName)
        {
            return $"Hello {userName}, you are logged in!";
        }

        #endregion
    }
}
