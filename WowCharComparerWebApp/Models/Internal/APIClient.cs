using System;
using System.ComponentModel.DataAnnotations;
using WowCharComparerWebApp.Models.Abstract;

namespace WowCharComparerWebApp.Models.Internal
{
    /// <summary>
    /// Information about API Client (they can be obtained from https://develop.battle.net)
    /// </summary>
    public class APIClient : DatabaseTableModel
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
