using Newtonsoft.Json;

namespace WowCharComparerWebApp.Models.RaiderIO.Character.MythicPlusRecentRuns
{
    public class Affixes
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonProperty(PropertyName = "wowhead_url")]
        public string WowHeadUrl { get; set; }
    }
}
