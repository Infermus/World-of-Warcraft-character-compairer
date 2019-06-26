using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WowCharComparerWebApp.Configuration;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Data.Database.Repository.User;
using WowCharComparerWebApp.Logic.User;
using WowCharComparerWebApp.Models.DataTransferObject;
using WowCharComparerWebApp.Notifications;

namespace WowCharComparerWebApp.Controllers.User
{
    public class LoginController : Controller
    {
        private readonly ComparerDatabaseContext _comparerDatabaseContext;
        private readonly PasswordValidationManager _passwordValidationManager;
        private readonly DbAccessUser _dbAccessUser;

        public LoginController(ComparerDatabaseContext comparerDatabaseContext, PasswordValidationManager passwordValidationManager, DbAccessUser dbAccessUser)
        {
            _comparerDatabaseContext = comparerDatabaseContext;
            _passwordValidationManager = passwordValidationManager;
            _dbAccessUser = dbAccessUser;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Login/PasswordRecovery")]
        [HttpGet]
        public IActionResult PasswordRecoveryView()
        {
            return View("PasswordRecovery");
        }

        [Route("Login/PasswordRecovery/Confirmation")]
        public IActionResult PasswordResetConfirmationView(string userID)
        {
            ViewBag.Message = userID;
            return View("PasswordChanged");
        }

        [HttpPost]
        public IActionResult PasswordChanged(string newPassword, string newPasswordConfirmation, Guid userID)
        {
            List<(bool, string)> checkedPasswords = new List<(bool, string)>();

            checkedPasswords.AddRange(_passwordValidationManager.CheckPassword(newPassword));
            checkedPasswords.AddRange(_passwordValidationManager.CheckPassword(newPasswordConfirmation));
            checkedPasswords.Add((newPassword.Equals(newPasswordConfirmation), UserMessages.UserConfirmPasswordNoMatch));

            if (checkedPasswords.All(x => x.Item1.Equals(true)))
            {
                _dbAccessUser.UpdatePassword(userID, newPassword);
                return Content("Password has been changed");
                //TODO  *Add generic view to display various messages with same layout* - TASK
            }
            else
            {
                return Content("Password must be between 8 and 30 characters. Contains one uppercase and one number digit");
                //TODO  *Add generic view to display various messages with same layout* - TASK
            }
        }

        [HttpPost]
        public IActionResult PasswordRecovery(string userEmail, string userName)
        {
            using (_comparerDatabaseContext)
            {
                Guid userID = _comparerDatabaseContext.Users.Where(user => user.Nickname.Equals(userName) && user.Email.Equals(userEmail))
                                                            .Select(user => user.ID)
                                                            .SingleOrDefault();

                string resetPasswordLink = $"{ HttpContext.Request.Protocol }://www.{ HttpContext.Request.Host}/Login/PasswordRecovery/Confirmation/?userID={userID.ToString()}";
                string recoveryPasswordSubject = "World of Warcraft Character Comparer: Password recovery!";
                string recoveryPasswordBody = $"<p> Hello {userName} </p>" +
                                              $"<p> You've asked to reset password for this World of Warcraft Character Comparer account: {userEmail} </p>" +
                                              "<p> Please follow link bellow to reset your password: </p>" +
                                              $"<a href=\"{resetPasswordLink}\">Confirmation </a>";

                bool userExists = _comparerDatabaseContext.Users.Any(user => user.Nickname.Equals(userName) && user.Email.Equals(userEmail));

                if (userExists == true)
                {
                    EmailSendStatus emailSendStatus = new EmailManager().SendMail(userEmail, recoveryPasswordSubject, recoveryPasswordBody);
                }
            }
            return View("PasswordRecoveryMailSended");
        }
    }
}