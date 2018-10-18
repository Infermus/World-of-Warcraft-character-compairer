using Newtonsoft.Json;

namespace WowCharComparerWebApp.Models.Achievement
{
    public class Categories : Achievements
    {
        [JsonProperty(PropertyName = "Categories")]
        public CategoriesData [] CategoriesData { get; set; }
    }
}
