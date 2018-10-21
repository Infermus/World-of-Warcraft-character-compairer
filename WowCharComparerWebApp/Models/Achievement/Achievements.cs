using Newtonsoft.Json;

namespace WowCharComparerWebApp.Models.Achievement
{ 
    public class Achievements 
    {
        [JsonProperty(PropertyName = "Id")]
        public int GlobalId { get; set; }

        [JsonProperty(PropertyName = "Achievements")]
        public AchievementsData [] AchievementsData { get; set; }

        [JsonProperty(PropertyName = "Categories")]
        public CategoriesData [] CategoriesData { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string GlobalName { get; set; }
    }
}
