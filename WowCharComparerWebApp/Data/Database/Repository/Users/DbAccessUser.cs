using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using WowCharComparerWebApp.Logic.Security;
using WowCharComparerWebApp.Models.DataTransferObject;
using WowCharComparerWebApp.Models.Internal;

namespace WowCharComparerWebApp.Data.Database.Repository.Users
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
        internal DbOperationStatus<User> CreateNew(string nickName, string password, string email)
        {
            DbOperationStatus<User> status = new DbOperationStatus<User>();

            User newUser = new User()
            {
                Nickname = nickName,
                Password = password,//new UserPasswordCryptography().EncryptUserPassword(password),
                Email = email,
                ID = new Guid(),
                IsOnline = false,
                LastLoginDate = DateTime.MinValue,
                RegistrationDate = DateTime.Now,
                Verified = false,
                VerificationToken = Guid.NewGuid()
            };

            _comparerDatabaseContext.Users.Add(newUser);

            status.RowsAffected = _comparerDatabaseContext.SaveChanges();
            status.OperationSuccess = true;
            status.ReturnedObject = newUser;

            return status;
        }

        /// <summary>
        /// Select user from database by username (nickname)
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        internal DbOperationStatus<User> GetUserByName(string userName)
        {
            DbOperationStatus<User> status = new DbOperationStatus<User>
            {
                QueryResult = _comparerDatabaseContext.Users.Where(u => u.Nickname.Equals(userName)).SingleOrDefault()
            };

            if (status.QueryResult == null)
            {
                status.OperationSuccess = false;
                _logger.LogWarning($"Can't find user \"{userName}\" in database");
            }
            else
            {
                status.ReturnedObject = status.QueryResult as User;
            }

            return status;
        }


        /// <summary>
        /// Activate user's account by set verified property to true
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="accountActivationGuid"></param>
        /// <returns></returns>
        internal DbOperationStatus<User> ActivateAccount(string accountActivationGuid)
        {
            DbOperationStatus<User> status = new DbOperationStatus<User>()
            {
                QueryResult = _comparerDatabaseContext.Users.Where(x => x.VerificationToken.ToString() == accountActivationGuid).SingleOrDefault(),
            };

            if (status.QueryResult != null)
            {
                (status.QueryResult as User).Verified = true;
                status.RowsAffected = _comparerDatabaseContext.SaveChanges();
                status.OperationSuccess = true;
            }
            else
            {
                status.OperationSuccess = false;
                _logger.LogWarning("Account activation failed - no user has been found");
            }

            return status;
        }

        internal DbOperationStatus<User> GetPassword(string username)
        {
            DbOperationStatus<User> status = new DbOperationStatus<User>
            {
                QueryResult = _comparerDatabaseContext.Users.Where(u => u.Nickname.Equals(username)).Single(),
            };

            status.OperationSuccess = status.QueryResult != null;

            if (status.OperationSuccess)
            {
                status.QueryResult = new UserPasswordCryptography().EncryptUserPassword((status.QueryResult as User).Password);
            }
            else
            {
                _logger.LogWarning($"Can't find password for user {username}");
            }

            return status;
        }

        /// <summary>
        /// Set new password to the user
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="newPassword">Password selected by user</param>
        /// <returns></returns>
        internal DbOperationStatus<User> UpdatePassword(Guid userID, string newPassword)
        {
            DbOperationStatus<User> status = new DbOperationStatus<User>();

            status.QueryResult = _comparerDatabaseContext.Users.Where(u => u.ID.Equals(userID))
                                                               .SingleOrDefault();
            if (status.QueryResult != null)
            {
                (status.QueryResult as User).Password = new UserPasswordCryptography().EncryptUserPassword(newPassword);
                status.RowsAffected = _comparerDatabaseContext.SaveChanges();
                status.OperationSuccess = true;
            }
            else
            {
                _logger.LogWarning("Update user's password - no user has been found");
                status.OperationSuccess = false;
            }

            return status;
        }
    }
}
