using Newtonsoft.Json;

namespace WowCharComparerWebApp.Models.CharacterProfile.StatisticsModels
{
    public class SubCategories : Model
    {
        [JsonProperty(PropertyName = "Statistics")]
        public Stats [] Stats { get; set; }

        [JsonProperty(PropertyName = "SubCategories")]
        public SubCat [] SubCat { get; set; }
    }
}
