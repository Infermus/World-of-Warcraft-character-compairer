using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using WowCharComparerWebApp.Models.Abstract;

namespace WowCharComparerWebApp.Models
{
    public class BonusStats : DatabaseTableModel
    {
        [JsonIgnore]
        [Key]
        public Guid ID { get; set; }

        [Required]
        [JsonProperty(PropertyName = "id")]
        public int BonusStatsID { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}