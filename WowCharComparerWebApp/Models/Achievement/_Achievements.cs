using Newtonsoft.Json;

namespace WowCharComparerWebApp.Models.Achievement
{ 
    public class Achievements
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "Achievements")]
        public AchievementsData [] AchievementsData { get; set; }

        public string Name { get; set; }
    }
}
