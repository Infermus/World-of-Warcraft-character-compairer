using Newtonsoft.Json;

namespace WowCharComparerLib.Models.CharacterProfile.Statistics
{
    public class SubCategories
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "Statistics")]
        public Stats [] Stats { get; set; }

        [JsonProperty(PropertyName = "SubCategories")]
        public SubCat [] SubCat { get; set; }
    }
}
