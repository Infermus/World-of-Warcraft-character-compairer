using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using WowCharComparerWebApp.Models.DataTransferObject;

namespace WowCharComparerWebApp.Data.Database.Repository.User
{
    public class DbAccessUser
    {
        private readonly ComparerDatabaseContext _comparerDatabaseContext;
        private readonly ILogger<DbAccessUser> _logger;

        public DbAccessUser(ComparerDatabaseContext comparerDatabaseContext, ILogger<DbAccessUser> logger)
        {
            _comparerDatabaseContext = comparerDatabaseContext;
            _logger = logger;
        }

        /// <summary>
        /// Create new user in database
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        internal Models.Internal.User CreateNew(string nickName, string password, string email)
        {
            Models.Internal.User newUser = new Models.Internal.User()
            {
                Nickname = nickName,
                Password = password,
                Email = email,
                ID = new Guid(),
                IsOnline = false,
                LastLoginDate = DateTime.MinValue,
                RegistrationDate = DateTime.Now,
                Verified = false,
                VerificationToken = Guid.NewGuid()
            };

            using (_comparerDatabaseContext)
            {
                _comparerDatabaseContext.Users.Add(newUser);
                _comparerDatabaseContext.SaveChanges();
            }

            return newUser;
        }

        /// <summary>
        /// Select user from database by username (nickname)
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        internal Models.Internal.User GetUserByName(string userName)
        {
            Models.Internal.User user = new Models.Internal.User();

            using (_comparerDatabaseContext)
            {
                user = _comparerDatabaseContext.Users.Where(u => user.Nickname.Equals(userName)).SingleOrDefault();

                if (user == null)
                {
                    _logger.LogWarning($"Canno't find user \"{userName}\" in database");
                    return null;
                }
            }

            return user;
        }


        /// <summary>
        /// Activate user's account by set verified property to true
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="accountActivationGuid"></param>
        /// <returns></returns>
        internal DbOperationStatus ActivateAccount(string accountActivationGuid)
        {
            DbOperationStatus status = new DbOperationStatus(typeof(Models.Internal.User));

            using (_comparerDatabaseContext)
            {
                var userAccount = _comparerDatabaseContext.Users.Where(user => user.VerificationToken.ToString().ToUpper().Equals(accountActivationGuid))
                                                                .SingleOrDefault();
                if (userAccount != null)
                {
                    userAccount.Verified = true;
                    status.RowsAffected = _comparerDatabaseContext.SaveChanges();
                    status.OperationSuccess = true;
                }
                else
                {
                    status.OperationSuccess = false;
                    _logger.LogWarning("Account activation failed - no user has been found");
                }
            }

            return status;
        }

        /// <summary>
        /// Set new password to the user
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="newPassword">Password selected by user</param>
        /// <returns></returns>
        internal DbOperationStatus UpdatePassword(Guid userID, string newPassword)
        {
            DbOperationStatus status = new DbOperationStatus(typeof(Models.Internal.User));

            using (_comparerDatabaseContext)
            {
                Models.Internal.User user = _comparerDatabaseContext.Users.Where(u => u.ID.Equals(userID))
                                                                          .SingleOrDefault();
                if (user != null)
                {
                    user.Password = newPassword;
                    status.RowsAffected = _comparerDatabaseContext.SaveChanges();
                    status.OperationSuccess = true;
                }
                else
                {
                    status.OperationSuccess = false;
                    _logger.LogWarning("Update user's password - no user has been found");
                }
            }

            return status;
        }
    }
}
