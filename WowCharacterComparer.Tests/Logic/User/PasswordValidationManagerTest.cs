using WowCharComparerWebApp.Configuration;
using WowCharComparerWebApp.Logic.User;
using Xunit;
using System.Linq;

namespace WowCharacterComparer.Test
{
    public class PasswordValidationManagerTest
    {
        [Theory]
        [InlineData("Infermus123@vp.pl", true, "")]
        [InlineData("Infermus123%vp.pl", false, UserMessages.UserEmailInvalidFormat)]
        public void CheckUserEmail_Validator_ShouldTestAllPossibilities(string userEmail, bool status, string message)
        {
            //Arrange 
            (bool, string) expected = (status, message);

            //Act
            var result = new PasswordValidationManager(null).CheckEmail(userEmail);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("TestUser", true, "")]
        [InlineData("TestU", false, UserMessages.UserNameLengthTooShort)]
        public void CheckUserName_Validator_ShouldTestAllPossibilities(string userName, bool status, string message)
        {
            //Arrange 
            (bool, string) expected = (status, message);

            //Act
            var result = new PasswordValidationManager(null).CheckUsername(userName).LastOrDefault();

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Qazwsx123", true, "")]
        [InlineData("qazwsx123", false, UserMessages.UserPasswordHasNoCapitalLetter)]
        [InlineData("QazwsxXX", false, UserMessages.UserPasswordHasNoNumbers)]
        [InlineData("Qaz5", false, UserMessages.UserPasswordLenghtTooShort)]
        public void CheckUserPassword_Validator_ShouldTestAllPossibilities(string password, bool status, string message)
        {
            //Arrange
            (bool, string) expected = (status, message);

            //Act
            var result = new PasswordValidationManager(null).CheckPassword(password).LastOrDefault();

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Qazwsx123", "Qazwsx123", true, "")]
        [InlineData("Qazwsx123", "Qazwsx1234", false, UserMessages.UserConfirmPasswordNoMatch)]
        public void CheckPasswordMatch_Validator_ShouldTestAllPossibilities(string userPassword, string userPasswordConfirmation, bool status, string message)
        {
            //Arrange
            (bool, string) expected = (status, message);

            //Act
            var result = new PasswordValidationManager(null).CheckPasswordMatch(userPassword, userPasswordConfirmation);

            //Assert
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("Infermus", "Qazwsx123", "Qazwsx123", "Infermus123@vp.pl" , true, "")]
        [InlineData(null, "Qazwsx123", "Qazwsx123", "Infermus123@vp.pl", false, UserMessages.UserNameIsRequired)]
        [InlineData("Infermus", null, "Qazwsx123", "Infermus123@vp.pl", false, UserMessages.PasswordIsRequired)]
        [InlineData("Infermus", "Qazwsx123", null, "Infermus123@vp.pl", false, UserMessages.PasswordConfirmationIsRequired)]
        [InlineData("Infermus", "Qazwsx123", "Qazwsx123", null, false, UserMessages.EmailIsRequired)]
        public void CheckIfInputIsEmpty_Validator_ShouldTestAllPossibilities(string userName, string userPassword, string confirmUserPassword, string userEmail, bool status, string message)
        {
            //Arrange
            (bool, string) expected = (status, message);

            //Act
            var result = new PasswordValidationManager(null).ValidateEmptyUserInput(userName, userPassword, confirmUserPassword, userEmail).LastOrDefault();

            //Assert
            Assert.Equal(result, expected);
        }
    }
}
