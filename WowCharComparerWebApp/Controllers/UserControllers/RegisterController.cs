using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WowCharComparerWebApp.Data.Database.Repository.Users;
using WowCharComparerWebApp.Logic.Users;
using WowCharComparerWebApp.Models.DataTransferObject;
using WowCharComparerWebApp.Models.Internal;
using WowCharComparerWebApp.Notifications;

namespace WowCharComparerWebApp.Controllers.UserControllers
{
    public class RegisterController : Controller
    {
        private readonly PasswordValidationManager _passwordValidationManager;
        private readonly DbAccessUser _dbAccessUser;
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(PasswordValidationManager passwordValidationManager, DbAccessUser dbAccessUser, ILogger<RegisterController> logger)
        {
            _passwordValidationManager = passwordValidationManager;
            _dbAccessUser = dbAccessUser;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Confirmation(string userID)
        {
            try
            {
                DbOperationStatus<User> operationStatus = _dbAccessUser.ActivateAccount(userID);

                if (!operationStatus.OperationSuccess)
                    throw new Exception("Error while activating account");
            }

            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return View("AccountApproval");
        }

        [HttpPost]
        public IActionResult PerformUserRegister(string accountName, string userPassword, string confirmUserPassword, string userEmail)
        {
            User user = new User();

            try
            {
                var emptyFieldsValidationResult = _passwordValidationManager.ValidateEmptyUserInput(accountName, userPassword, confirmUserPassword, userEmail);

                if (emptyFieldsValidationResult.Any(x => x.Item1 == false))
                {
                    // Return information to user about empty input fields
                    return Content(string.Concat(emptyFieldsValidationResult.Select(x => x.Item2 + Environment.NewLine)));
                }

                List<(bool, string)> passwordPolicyValidation = new List<(bool, string)>();

                passwordPolicyValidation.AddRange(_passwordValidationManager.CheckUsername(accountName));
                passwordPolicyValidation.AddRange(_passwordValidationManager.CheckPassword(userPassword));
                passwordPolicyValidation.Add(_passwordValidationManager.CheckPasswordMatch(userPassword, confirmUserPassword));
                passwordPolicyValidation.Add(_passwordValidationManager.CheckEmail(userEmail));

                if (passwordPolicyValidation.All(x => x.Item1))
                {
                    user = _dbAccessUser.CreateNew(accountName, userPassword, userEmail).ReturnedObject as User;
                }
                else
                {
                    // Return information to user why registration fails
                    return Content(passwordPolicyValidation.Where(v => v.Item1 == false).Select(s => s.Item2).ToArray().First());
                }

                string activationLink = Url.Action("Confirmation", "Register", new
                {
                    userID = user.VerificationToken.ToString()

                }, protocol: HttpContext.Request.Scheme);

                EmailSendStatus emailSendStatus = new EmailManager().SendMail(user.Email, "World of Warcraft Character Comparer: Verify account!",
                                                      $"<p> Thank you for registration {user.Nickname}." +
                                                      $"<p>To verify your account please click on following link:</p>" +
                                                      $"<a href=\"{activationLink}\">Activate my account!</a>");

                if (!emailSendStatus.SendSuccessfully)
                {
                    _dbAccessUser.RemoveByID(user.ID);
                    _logger.LogInformation($"Removing user from database {user.Nickname}, {user.Email}, {user.ID}");
                    _logger.LogError($"Error while sending activation email. {emailSendStatus.SendEmailException.Message}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occour while registering user. {ex.Message}");
                return View("Error", ex);
            }

            return View("UserRegistrationCompleted", user);
        }
    }
}