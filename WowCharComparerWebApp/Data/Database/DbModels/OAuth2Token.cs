using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WowCharComparerWebApp.Data.Database.DbModels
{
    public class OAuth2Token
    {
        [Key]
        public Guid ID { get; set; }

        /// <summary>
        /// Name of the totem (can be any, it's just for developer identification)
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string TokenName { get; set; }

        /// <summary>
        /// Token value - this part is added to the header to get data from API (otherwise user is unauthorized)
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string TokenValue { get; set; }

        /// <summary>
        /// Expires time in sec
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Type of token
        /// </summary>
        public string TokenType { get; set; }
    }
}
