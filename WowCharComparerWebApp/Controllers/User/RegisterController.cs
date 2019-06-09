using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
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
                            Verified = false
                        };

                        SendVerificationMail(userEmail);

                        db.Users.Add(user);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                }
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
            return email.Contains("@") && email.Contains(".");
        }

        public void SendVerificationMail(string userEmail)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(APIConf.WowCharacterComparerEmail, APIConf.WoWCharacterComparerEmailPassword)
            };

            MailMessage msg = new MailMessage();
            msg.To.Add(userEmail);
            msg.From = new MailAddress(APIConf.WowCharacterComparerEmail);
            msg.Subject = "Verification";
            msg.Body = "This is a email to verification your World of Warcraft Character Comparer account";
            client.Send(msg);
        }
    }
}