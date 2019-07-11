using System;
using System.ComponentModel.DataAnnotations;
using WowCharComparerWebApp.Models.Abstract;

namespace WowCharComparerWebApp.Models.Internal
{
    public class User : DatabaseTableModel
    {
        /// <summary>
        /// Unique ID for User 
        /// </summary>
        [Key]
        [Required]
        public Guid ID { get; set; }

        /// <summary>
        /// User's login
        /// </summary>
        [MaxLength(30)]
        [Required]
        public string Nickname { get; set; }

        /// <summary>
        /// Random sequence of bytes (Salt)
        /// </summary>
        [MaxLength(64)]
        [Required]
        public string Salt { get; set; }

        /// <summary>
        /// Hashed password with salt
        /// </summary>
        [MaxLength(64)]
        [Required]
        public string HashedPassword { get; set; }

        /// <summary>
        /// User's Email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Variable for checking if user verified his email
        /// </summary>
        [Required]
        public bool Verified { get; set; }

        /// <summary>
        /// Date of user registration
        /// </summary>
        [Required]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Datestep - last time when user was online
        /// </summary>
        [Required]
        public DateTime LastLoginDate { get; set; }

        /// <summary>
        /// User online status
        /// </summary>
        [Required]
        public bool IsOnline { get; set; }

        /// <summary>
        /// Verification Token for activate user account
        /// </summary>
        [Required]
        public Guid VerificationToken { get; set; }

        /// <summary>
        /// Password recovery expiration time
        /// </summary>
        [Required]
        public DateTime PasswordRecoveryExpirationTime { get; set; }
    }
}