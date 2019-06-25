using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Notifications;

namespace WowCharComparerWebApp.Controllers.User
{
    public class LoginController : Controller
    {
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
            List<string> passwordsToCheck = new List<string>();
            {
                passwordsToCheck.Add(newPassword);
                passwordsToCheck.Add(newPasswordConfirmation);
            }

            List<bool> checkedPasswords = new List<bool>();

            for (int index = 0; index < passwordsToCheck.Count(); index ++)
                    checkedPasswords.Add(RegisterController.CheckPassword(passwordsToCheck[index]));

            if (checkedPasswords.All(x => x.Equals(true)))
            {
                UpdateUserPassword(newPassword,userID);
                return Content("Password has been changed");
                //TODO  *Add generic view to display various messages with same layout* - TASK
            }
            else
            {
                return Content("Password must be between 8 and 30 characters. Contains one uppercase and one number digit");
                //TODO  *Add generic view to display various messages with same layout* - TASK
            }
        }

        private void UpdateUserPassword(string newPassword, Guid userID)
        {
            using (ComparerDatabaseContext db = new ComparerDatabaseContext())
            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                var userAccount = db.Users.Where(user => user.ID.Equals(userID))
                                       .SingleOrDefault();

                userAccount.Password = newPassword;
                transaction.Commit();
                db.SaveChanges();
            }
        }

        [HttpPost]
        public IActionResult PasswordRecovery(string userEmail, string userName)
        {
            using (ComparerDatabaseContext db = new ComparerDatabaseContext())
            {
                var userID = db.Users.Where(user => user.Nickname.Equals(userName) && user.Email.Equals(userEmail)).Select(user => user.ID).ToList();
                string protocol = HttpContext.Request.IsHttps ? "https" : "http";
                string resetPasswordLink = $"{protocol}://www.{ HttpContext.Request.Host}/Login/PasswordRecovery/Confirmation/?userID={userID[0]}";
                string recoveryPasswordSubject = "World of Warcraft Character Comparer: Password recovery!";
                string recoveryPasswordBody = $"<p> Hello {userName} </p>" +
                                              $"<p> You've asked to reset password for this World of Warcraft Character Comparer account: {userEmail} </p>" +
                                              "<p> Please follow link bellow to reset your password: </p>" +
                                              $"<a href=\"{resetPasswordLink}\">Confirmation </a>";

                bool userExists = db.Users.Any(user => user.Nickname.Equals(userName) && user.Email.Equals(userEmail));
                EmailManager emailMannager = new EmailManager();
               
                if (userExists == true)
                {
                    emailMannager.SendMail(userEmail, recoveryPasswordSubject, recoveryPasswordBody);
                }
            }
            return View("PasswordRecoveryMailSended");
        }
    }
}