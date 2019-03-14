using Newtonsoft.Json;

namespace WowCharComparerWebApp.Models.Achievement
{
    public class Achievement
    {
        [JsonProperty(PropertyName = "Achievements")]
        public AchievementCategory[] AchievementCategory { get; set; }
    }
}