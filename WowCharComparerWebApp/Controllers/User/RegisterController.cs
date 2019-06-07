using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WowCharComparerWebApp.Data.Database;
using System.Timers;

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
            using (ComparerDatabaseContext db = new ComparerDatabaseContext())
            {
                using (IDbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    List<bool> CheckedValidators = new List<bool>()
                    {
                        CheckUsername(accountName,db),
                        CheckPassword(userPassword),
                        CheckEmail(userEmail,db)

                    };

                    if (CheckedValidators.All(x => x))
                    {
                            db.Users.Add(new Models.Internal.User()
                            {
                                Email = userEmail,
                                Password = userPassword,
                                Nickname = accountName,
                                ID = new Guid(),
                                IsOnline = false,
                                LastLoginDate = DateTime.MinValue,
                                RegistrationDate = DateTime.Now,
                                Verified = false
                            });

                        db.SaveChanges();
                        transaction.Commit();
                    }
                }
            }

            return View();
        }

        public bool CheckPassword(string password)
        {
            bool checkedPassword = password.Any(char.IsDigit) & password.Any(char.IsUpper) & password.Length >= 8
                                                              ? true
                                                              : false;

            return checkedPassword;
        }

        public bool CheckUsername(string accountName, ComparerDatabaseContext db)
        {
            bool checkedAccountName = accountName.Length >= 6 && db.Users.All(x => x.Nickname != accountName) 
                                                               ? true
                                                               : false;


            return checkedAccountName;
        }

        public bool CheckEmail(string email, ComparerDatabaseContext db)
        {
            bool checkedEmail = email.Contains("@") && email.Contains(".");
            return checkedEmail;
        }
    }
}