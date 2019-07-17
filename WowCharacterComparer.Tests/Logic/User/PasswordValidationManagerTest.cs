using WowCharComparerWebApp.Configuration;
using Xunit;
using System.Linq;
using WowCharComparerWebApp.Logic.Users;

namespace WowCharacterComparer.Test
{
    public class PasswordValidationManagerTest
    {
        [Fact]
        public void CheckUserEmail_WhenUserEmailIsCorrect_ShouldReturnCorrectResult()
        {
            //Arrange 
            (bool, string) expected = (true, string.Empty);
            string userEmail = "Infermus123@vp.pl";

            //Act
            var result = new PasswordValidationManager(null, null).CheckEmail(userEmail);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CheckUserEmail_WhenUserEmailIsInvalid_ShouldReturnErrorWithSuitableMessage()
        {
            //Arrange 
            (bool, string) expected = (false, UserMessages.EmailInvalidFormat);
            string userEmail = "Infermus123%vp.pl";

            //Act
            var result = new PasswordValidationManager(null, null).CheckEmail(userEmail);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CheckUserName_WhenUserNameIsInvalid_ShouldReturnErrorWithSuitableMessage()
        {
            //Arrange 
            (bool, string) expected = (false, UserMessages.NameLengthTooShort);
            string userName = "TestU";

            //Act
            var result = new PasswordValidationManager(null, null).CheckUsername(userName).LastOrDefault();

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CheckUserName_WhenUserNameIsCorrect_ShouldReturnCorrectResult()
        {
            //Arrange 
            (bool, string) expected = (true, string.Empty);
            string userName = "TestUser";

            //Act
            var result = new PasswordValidationManager(null, null).CheckUsername(userName).LastOrDefault();

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("qazwsx123", false, UserMessages.PasswordHasNoCapitalLetter)]
        [InlineData("QazwsxXX", false, UserMessages.PasswordHasNoNumbers)]
        [InlineData("Qaz5", false, UserMessages.PasswordLenghtTooShort)]
        public void CheckUserPassword_WhenUserPasswordIsInvalid_ShouldReturnErrorWithSuitableMessage(string password, bool status, string message)
        {
            //Arrange
            (bool, string) expected = (status, message);

            //Act
            var result = new PasswordValidationManager(null, null).CheckPassword(password).LastOrDefault();

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CheckUserPassword_WhenUserPasswordIsCorrect_ShouldReturnCorrectResult()
        {
            //Arrange
            (bool, string) expected = (true, string.Empty);
            string userPassword = "Qazwsx123";

            //Act
            var result = new PasswordValidationManager(null, null).CheckPassword(userPassword).LastOrDefault();

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CheckPasswordMatch_WhenUserPasswordIsMatching_ShouldReturnCorrectResult()
        {
            //Arrange
            (bool, string) expected = (true, string.Empty);
            string userPassword = "Qazwsx123";
            string userPasswordConfirmation = "Qazwsx123";

            //Act
            var result = new PasswordValidationManager(null, null).CheckPasswordMatch(userPassword, userPasswordConfirmation);

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void CheckPasswordMatch_WhenUserPasswordNoMatch_ShouldReturnErrorWithSuitableMessage()
        {
            //Arrange
            (bool, string) expected = (false, UserMessages.ConfirmPasswordNoMatch);
            string userPassword = "Qazwsx123";
            string userPasswordConfirmation = "Qazwsx1234";

            //Act
            var result = new PasswordValidationManager(null, null).CheckPasswordMatch(userPassword, userPasswordConfirmation);

            //Assert
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData(null, "Qazwsx123", "Qazwsx123", "Infermus123@vp.pl", false, UserMessages.UserNameIsRequired)]
        [InlineData("Infermus", null, "Qazwsx123", "Infermus123@vp.pl", false, UserMessages.PasswordIsRequired)]
        [InlineData("Infermus", "Qazwsx123", null, "Infermus123@vp.pl", false, UserMessages.PasswordConfirmationIsRequired)]
        [InlineData("Infermus", "Qazwsx123", "Qazwsx123", null, false, UserMessages.EmailIsRequired)]
        public void ValidateEmptyUserInput_WhenUserIsTryingToPassNullData_ShouldReturnErrorWithSuitableMessage(string userName, string userPassword, string confirmUserPassword, string userEmail, bool status, string message)
        {
            //Arrange
            (bool, string) expected = (status, message);

            //Act
            var result = new PasswordValidationManager(null, null).ValidateEmptyUserInput(userName, userPassword, confirmUserPassword, userEmail).LastOrDefault();

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void ValidateEmptyUserInput_WhenUserFilledAllFields_ShouldReturnCorrectResult()
        {
            //Arrange
            (bool, string) expected = (true, string.Empty);
            string userName = "Infermus";
            string userPassword = "Qazwsx123";
            string confirmUserPassword = "Qazwsx123";
            string userEmail = "Infermus123@vp.pl";

            //Act
            var result = new PasswordValidationManager(null, null).ValidateEmptyUserInput(userName, userPassword, confirmUserPassword, userEmail).LastOrDefault();

            //Assert
            Assert.Equal(result, expected);
        }
    }
}
