
namespace WowCharComparerWebApp.Configuration
{
    public static class UserMessages
    {
        public const string UserPasswordHasNoNumbers = "Password should contains at least one number";
        public const string UserPasswordHasNoCapitalLetter = "Password should contains at least one capital letter";
        public const string UserConfirmPasswordNoMatch = "Passwords no match";
        public const string UserPasswordLenghtTooShort = "Password should be at least 8 characters long";
        public const string UserNameLengthTooShort = "User name should be at least 6 characters long";
        public const string UserAlreadyExists = "User name already exists";
        public const string UserEmailInvalidFormat = "Email address has invalid format";

        public const string UserNameIsRequired = "User name is required";
        public const string PasswordIsRequired = "Password is required";
        public const string PasswordConfirmationIsRequired = "Password confirmation is required";
        public const string EmailIsRequired = "Email is required";
    }
}
