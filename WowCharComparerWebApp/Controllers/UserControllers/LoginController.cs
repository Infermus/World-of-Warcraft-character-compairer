using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WowCharComparerWebApp.Configuration;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Data.Database.Repository.Users;
using WowCharComparerWebApp.Logic.Users;
using WowCharComparerWebApp.Models.DataTransferObject;
using WowCharComparerWebApp.Notifications;

namespace WowCharComparerWebApp.Controllers.UserControllers
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
        public IActionResult PasswordRecovery()
        {
            return View("PasswordRecovery");
        }

        public IActionResult PasswordResetConfirmation(Guid userID)
        {
            TempData["userID"] = userID;
            return View("ChangeUserPassword");
        }

        [HttpPost]
        [Route("Login/Authenticate")]
        public IActionResult Authenticate(string userName, string password)
        {
            var user = _dbAccessUser.GetUserByName(userName).ReturnedObject;

            if (user == null)
                return Content($"User {userName} is not registered");

            if (!_passwordValidationManager.UserLoginPasswordMatch(user, password))
                return Content(UserMessages.InvalidPassword);

            return Content($"Hello {userName}, you are logged in!");
        }

        [HttpPost]
        public IActionResult ChangeUserPassword(string newPassword, string newPasswordConfirmation)
        {
            Guid userID = ViewBag.Message = TempData["userID"];
            var user = _dbAccessUser.GetUserByGuid(userID).ReturnedObject;

            if (DateTime.Now < user.PasswordRecoveryExpirationTime)
            {
                if (!newPassword.Equals(newPasswordConfirmation))
                    return Content(UserMessages.ConfirmPasswordNoMatch);

                if ((_passwordValidationManager.CheckPassword(newPassword).Any(x => !x.Item1)))
                    return Content(_passwordValidationManager.CheckPassword(newPassword).FirstOrDefault().Item2);

                if ((_passwordValidationManager.CheckPassword(newPasswordConfirmation).Any(x => !x.Item1)))
                    return Content(_passwordValidationManager.CheckPassword(newPasswordConfirmation).FirstOrDefault().Item2);

                _dbAccessUser.UpdatePassword(userID, newPassword);

                return Content("Password has been changed");
            }

            return Content("Time for changing password run out.");
        }

        [HttpPost]
        public IActionResult PasswordRecovery(string userEmail, string userName)
        {
            var user = _dbAccessUser.GetUserByName(userName).ReturnedObject;

            if (user == null)
            {
                return Content($"Cannot find user {userName}. Password recovery failed");
            }

            string resetPasswordLink = Url.Action("PasswordResetConfirmation", "Login", new
            {
                userID = user.ID

            }, protocol: HttpContext.Request.Scheme);

            string recoveryPasswordSubject = "World of Warcraft Character Comparer: Password recovery!";
            string recoveryPasswordBody = $"<p> Hello {userName} </p>" +
                                          $"<p> You've asked to reset password for this World of Warcraft Character Comparer account: {userEmail} </p>" +
                                          $"<p> Link gonna be validate for {ExpirationTimers.PasswordRecovery} hours</p>" +
                                          "<p> Please follow link bellow to reset your password: </p>" +
                                          $"<a href=\"{resetPasswordLink}\">Confirmation </a>";

            EmailSendStatus emailSendStatus = new EmailManager().SendMail(userEmail, recoveryPasswordSubject, recoveryPasswordBody);
            _dbAccessUser.SetExpirationTime(ExpirationTimers.PasswordRecovery, user);

            if (emailSendStatus.SendSuccessfully == true)
            {
                _dbAccessUser.SetExpirationTime(ExpirationTimers.PasswordRecovery, user);
            }
            
            else
            {
                return View("Error");
            }

            return View("PasswordRecoveryMailSended");
        }
    }
}