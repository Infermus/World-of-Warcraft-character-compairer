using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Models.DataTransferObject;
using WowCharComparerWebApp.Notifications;

namespace WowCharComparerWebApp.Controllers.User
{
    public class RegisterController : Controller
    {
        private ILogger _logger;

        public RegisterController(ILogger<RegisterController> logger)
        {
            _logger = logger;
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
        public IActionResult PerformUserRegister(string accountName, string userPassword, string userEmail)
        {
            Models.Internal.User user = new Models.Internal.User();
            try
            {
                using (ComparerDatabaseContext db = new ComparerDatabaseContext())
                using (IDbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    List<bool> CheckedValidators = new List<bool>()
                    {
                        CheckUsername(accountName, db),
                        CheckPassword(userPassword),
                        CheckEmail(userEmail, db)
                    };

                    if (CheckedValidators.All(x => x))
                    {
                        user = new Models.Internal.User()
                        {
                            Email = userEmail,
                            Password = userPassword,
                            Nickname = accountName,
                            ID = new Guid(),
                            IsOnline = false,
                            LastLoginDate = DateTime.MinValue,
                            RegistrationDate = DateTime.Now,
                            Verified = false,
                            VerificationToken = Guid.NewGuid()
                        };

                        db.Users.Add(user);
                        db.SaveChanges();
                        transaction.Commit();     
                    }
                }

                string protocol = HttpContext.Request.IsHttps ? "https" : "http";
                string activationLink = $"{protocol}://www.{ HttpContext.Request.Host}/Register/AccountConfirmation";
                string messageBody = "<p>This is a email to verification your World of Warcraft Character Comparer account.</p>" +
                                    $"<p> Activation code: <b> {user.VerificationToken.ToString()} </b> </p>" +
                                     "Please active your account " + 
                                    $"<a href=\"{activationLink}\">here </a>";
                string messageSubject = "World of Warcraft Character Comparer: Verify account!";

                EmailManager emailMannager = new EmailManager();
                EmailSendStatus emailSendStatus = emailMannager.SendMail(user.Email, messageSubject, messageBody);

                if (emailSendStatus.SendSuccessfully == false)
                {
                    // TODO return message for failed email send
                }
            }

            catch (Exception ex)
            {
                // return HTML view which shows the user that he cannot register because of technial problem
                return StatusCode(501);
            }
            return View(user);
        }

        public static bool CheckPassword(string password)
        {
            return password.Any(char.IsDigit) & password.Any(char.IsUpper) & password.Length >= 8;
        }

        public bool CheckUsername(string accountName, ComparerDatabaseContext db)
        {
            return accountName.Length >= 6 & db.Users.All(x => x.Nickname != accountName);
        }

        public bool CheckEmail(string email, ComparerDatabaseContext db)
        {
            if (db.Users.All(x => x.Email != email))
            {
                try
                {
                    MailAddress mail = new MailAddress(email);
                    return true;
                }

                catch (Exception ex)
                {
                    //TODO write to user that mail format is incorrect or already is in database
                    return false;
                }
            }
            else
                return false;
        }

        [Route("Register/ConfirmUserAccount")]
        [HttpPost]
        public IActionResult ConfirmUserAccount(string userAccountActivationToken)
        {
            try
            {
                using (ComparerDatabaseContext db = new ComparerDatabaseContext())
                using (IDbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    var userAccount = db.Users.Where(user => user.VerificationToken.ToString().ToUpper().Equals(userAccountActivationToken))
                                           .SingleOrDefault();
                    if(userAccount != null)
                        userAccount.Verified = true;

                    transaction.Commit();
                    db.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                return View("Error");
            }

            return View("AccountApproval");
        }
    }
}