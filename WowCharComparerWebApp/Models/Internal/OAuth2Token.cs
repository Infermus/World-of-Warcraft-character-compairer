using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WowCharComparerWebApp.Models.Internal
{
    public class OAuth2Token
    { 
        /// <summary>
        /// Token value - this part is added to the header to get data from API (otherwise user is unauthorized)
        /// </summary>
        [Required]
        [MaxLength(50)] 
        [JsonProperty(PropertyName ="access_token")]
        public string TokenValue { get; set; }

        /// <summary>
        /// Expires time in sec
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Type of token
        /// </summary>
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
    }
}
