using System;
using System.ComponentModel.DataAnnotations;

namespace WowCharComparerWebApp.Models.Internal
{
    public class APIClient
    {
        [Key]
        [Required]
        public string ClientID { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string ClientName { get; set; }

        [Required]
        public DateTime ValidationUntil { get; set; }
    }
}
