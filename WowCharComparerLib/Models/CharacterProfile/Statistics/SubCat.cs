using Newtonsoft.Json;

namespace WowCharComparerLib.Models.CharacterProfile.Statistics
{
    public class SubCat : Model
    {
        [JsonProperty(PropertyName = "Statistics")]
        public Stats [] Stats { get; set; }
    }
}
