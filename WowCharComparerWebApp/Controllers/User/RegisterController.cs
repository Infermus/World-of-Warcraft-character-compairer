using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Data.Database.Repository.User;
using WowCharComparerWebApp.Logic.User;
using WowCharComparerWebApp.Models.DataTransferObject;
using WowCharComparerWebApp.Notifications;

namespace WowCharComparerWebApp.Controllers.User
{
    public class RegisterController : Controller
    {
        private readonly PasswordValidationManager _passwordValidationManager;
        private readonly DbAccessUser _dbAccessUser;

        public RegisterController(PasswordValidationManager passwordValidationManager, DbAccessUser dbAccessUser)
        {
            _passwordValidationManager = passwordValidationManager;
            _dbAccessUser = dbAccessUser;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Register/AccountConfirmation")]
        [HttpGet]
        public IActionResult Confirmation()
        {
            return View("AccountConfirmation");
        }

        [HttpPost]
        public IActionResult PerformUserRegister(string accountName, string userPassword, string confirmUserPassword, string userEmail)
        {
            Models.Internal.User user = new Models.Internal.User();

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
                    user = _dbAccessUser.CreateNew(accountName, userPassword, userEmail);
                }
                else
                {
                    // Return information to user why registration fails
                    return Content(passwordPolicyValidation.Where(v => v.Item1 == false).Select(s => s.Item2).ToArray().First());
                }

                string protocol = HttpContext.Request.IsHttps ? "https" : "http";
                string activationLink = $"{protocol}://www.{HttpContext.Request.Host}/Register/AccountConfirmation";

                EmailSendStatus emailSendStatus = new EmailManager().SendMail(user.Email, "World of Warcraft Character Comparer: Verify account!",
                                                      "<p> This is a email to verification your World of Warcraft Character Comparer account.</p>" +
                                                     $"<p> Activation code: <b> {user.VerificationToken.ToString()} </b> </p>" +
                                                      "Please active your account " + $"<a href=\"{activationLink}\">here </a>");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }

            return View("UserRegistrationCompleted", user);
        }

        [Route("Register/ConfirmUserAccount")]
        [HttpPost]
        public IActionResult ConfirmUserAccount(string userAccountActivationToken)
        {
            try
            {
                DbOperationStatus operationStatus = _dbAccessUser.ActivateAccount(userAccountActivationToken);
            }

            catch (Exception ex)
            {
                return View("Error", ex);
            }

            return View("AccountApproval");
        }
    }
}