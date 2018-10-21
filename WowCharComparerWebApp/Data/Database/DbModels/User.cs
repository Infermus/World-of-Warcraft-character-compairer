using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WowCharComparerWebApp.Data.Database.DbModels
{
    public class User
    {
        /// <summary>
        /// Uniquie ID for User 
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
        /// User's Password (encrypted)
        /// </summary>
        [MaxLength(30)]
        [Required]
        public string Password { get; set; }

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
    }
}
