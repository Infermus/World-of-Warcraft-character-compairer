using Newtonsoft.Json;
using System.Collections.Generic;

namespace WowCharComparerWebApp.Models.Achievement
{
    public class Achievement
    {
        [JsonProperty(PropertyName = "Achievements")]
        public ICollection<AchievementCategory> AchievementCategory { get; set; }
    }
}