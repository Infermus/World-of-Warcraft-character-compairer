using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace WowCharComparerWebApp.Models
{
    public class BonusStats
    {
        [JsonIgnore]
        [Key]
        public Guid ID { get; set; }

        [Required]
        [JsonProperty(PropertyName = "Id")]
        public int BonusStatsID { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}