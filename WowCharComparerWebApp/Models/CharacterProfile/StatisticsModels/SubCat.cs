using Newtonsoft.Json;

namespace WowCharComparerWebApp.Models.CharacterProfile.StatisticsModels
{
    public class SubCat : Model
    {
        [JsonProperty(PropertyName = "Statistics")]
        public Stats [] Stats { get; set; }
    }
}
