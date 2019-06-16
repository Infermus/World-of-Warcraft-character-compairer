using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using WowCharComparerWebApp.Configuration;
using WowCharComparerWebApp.Data.Database;

namespace WowCharComparerWebApp.Controllers.User
{
    public class RegisterController : Controller
    {
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
                        //TODO improve validators
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
                SendVerificationMail(userEmail, user.VerificationToken);
            }
            catch (Exception ex)
            {
                // return HTML view which shows the user that he cannot register because of technial problem
            }

            return View(user);
        }

        public bool CheckPassword(string password)
        {
            return password.Any(char.IsDigit) & password.Any(char.IsUpper) & password.Length >= 8;
        }

        public bool CheckUsername(string accountName, ComparerDatabaseContext db)
        {
            return accountName.Length >= 6 && db.Users.All(x => x.Nickname != accountName);
        }

        public bool CheckEmail(string email, ComparerDatabaseContext db)
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

        public void SendVerificationMail(string userEmail, Guid activationGuid)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(APIConf.WowCharacterComparerEmail, APIConf.WoWCharacterComparerEmailPassword),
                Timeout = 100000,
                EnableSsl = true
            };

            string protocol = HttpContext.Request.IsHttps ? "https" : "http";

            string activationLink = $"{protocol}://www.{ HttpContext.Request.Host}/Register/AccountConfirmation";

            MailMessage msg = new MailMessage();
            msg.To.Add(userEmail);
            msg.From = new MailAddress(APIConf.WowCharacterComparerEmail);
            msg.IsBodyHtml = true;
            msg.Subject = "World of Warcraft Character Comparer: Verify account!";
            msg.Body = "<p> This is a email to verification your World of Warcraft Character Comparer account.</p>" +
                       $"<p> Activation code: <b> {activationGuid.ToString()} </b> </p>" +
                       "Please active your account " + $"<a href=\"{activationLink}\">here </a>";
            client.Send(msg);
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