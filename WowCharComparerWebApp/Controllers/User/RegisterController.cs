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
        public IActionResult PerformUserRegister(string accountName, string userPassword, string confirmUserPassword, string userEmail)
        {
            Models.Internal.User user = new Models.Internal.User();

            try
            {
                // First of all - validate if there is any missing input fields skipped by user
                if (ValidateEmptyUserInput(accountName, userPassword, confirmUserPassword, userEmail).Any(x => x.Item1 == false))
                {
                    return View("Index"); // TODO rework it to show user error
                }

                using (ComparerDatabaseContext db = new ComparerDatabaseContext())
                using (IDbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    List<(bool, string)> checkedValidators = new List<(bool, string)>();
      
                    checkedValidators.AddRange(CheckUsername(accountName, db));
                    checkedValidators.AddRange(CheckPassword(userPassword));
                    checkedValidators.Add(CheckPasswordMatch(userPassword, confirmUserPassword));
                    checkedValidators.Add(CheckEmail(userEmail, db));

                    if (checkedValidators.All(x => x.Item1))
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
                    else
                    {
                        string[] userInvalidMessages = checkedValidators.Where(v => v.Item1 == false).Select(s => s.Item2).ToArray();
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

        /// <summary>
        /// Determines if any of user input is null or empty 
        /// </summary>
        /// <param name="username">Username typed by user</param>
        /// <param name="userPassword">Password typed by user</param>
        /// <param name="confirmUserPassword">Password confirmation typed by user</param>
        /// <param name="userEmail">Email address typed by user</param>
        /// <returns>Validation list where first param (bool) is validation correct, second param (string) is message </returns>
        private List<(bool, string)> ValidateEmptyUserInput(string username, string userPassword, string confirmUserPassword, string userEmail)
        {
            List<(bool, string)> emptyFieldsValidator = new List<(bool, string)>()
            {
                (true, string.Empty)
            };

            if (string.IsNullOrEmpty(username))
            {
                emptyFieldsValidator.Add((false, UserMessages.UsernameIsRequired));
            }

            if (string.IsNullOrEmpty(userPassword))
            {
                emptyFieldsValidator.Add((false, UserMessages.PasswordIsRequired));
            }

            if (string.IsNullOrEmpty(confirmUserPassword))
            {
                emptyFieldsValidator.Add((false, UserMessages.PasswordConfirmationIsRequired));
            }

            if (string.IsNullOrEmpty(userEmail))
            {
                emptyFieldsValidator.Add((false, UserMessages.EmailIsRequired));
            }

            return emptyFieldsValidator;
        }

        /// <summary>
        /// Determines if user password field match the confirm password field 
        /// </summary>
        /// <param name="password">Primary password passed by user</param>
        /// <param name="confirmPasword">Secondary (confirmation) password passed by user</param>
        /// <returns>First param (bool) - is validation correct, Second param (string) - message</returns>
        public (bool, string) CheckPasswordMatch(string password, string confirmPasword)
        {
            return password.Equals(confirmPasword) ? (true, string.Empty) : (false, UserMessages.UserConfirmPasswordNoMatch);
        }

        /// <summary>
        /// Checks password validation from user input
        /// </summary>
        /// <param name="password">Password assigned by user</param>
        /// <returns>Validation list where first param (bool) is validation correct, second param (string) is message </returns>
        public List<(bool, string)> CheckPassword(string password)
        {
            List<(bool, string)> passValidator = new List<(bool, string)>()
            {
                (true, string.Empty)
            };

            if (!password.Any(char.IsDigit))
            {
                passValidator.Add((false, UserMessages.UserConfirmPasswordNoMatch));
            }

            if (!password.Any(char.IsUpper))
            {
                passValidator.Add((false, UserMessages.UserPasswordHasNoCapitalLetter));
            }

            if (!(password.Length >= 8))
            {
                passValidator.Add((false, UserMessages.UserPasswordLenghtTooShort));
            }

            return passValidator;
        }

        /// <summary>
        /// Checks username validation for user input
        /// </summary>
        /// <param name="username">Username typed by user</param>
        /// <param name="db">Database context</param>
        /// <returns>Validation list where first param (bool) is validation correct, second param (string) is message </returns>
        public List<(bool, string)> CheckUsername(string username, ComparerDatabaseContext db)
        {
            List<(bool, string)> usernameValidator = new List<(bool, string)>()
            {
                (true, string.Empty)
            };

            if (!(username.Length >= 6))
            {
                usernameValidator.Add((false, UserMessages.UserNameLengthTooShort));
            }

            if (!db.Users.All(x => x.Nickname != username))
            {
                usernameValidator.Add((false, UserMessages.UserAlreadyExists));
            }

            return usernameValidator;
        }

        /// <summary>
        /// Checks email validation format for user input
        /// </summary>
        /// <param name="email">User input email</param>
        /// <param name="db">Database context</param>
        /// <returns>First param (bool) - is validation correct, Second param (string) - message</returns>
        public (bool, string) CheckEmail(string email, ComparerDatabaseContext db)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                //TODO write to user that mail format is incorrect or already is in database
                return (false, UserMessages.UserEmailInvalidFormat);
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