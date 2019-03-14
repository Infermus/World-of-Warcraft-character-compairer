
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace WowCharComparerWebApp.Models.Achievement
{
    public class AchievementsSubCategoryData
    {
        [Key]
        [JsonProperty(PropertyName = "Id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "Achievements")]
        public AchievementsData[] AchievementsData { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string CategoryName { get; set; }
    }
}