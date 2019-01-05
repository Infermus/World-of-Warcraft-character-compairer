using Newtonsoft.Json;
using WowCharComparerWebApp.Models.RaiderIO.Character.MythicPlusRecentRuns;

namespace WowCharComparerWebApp.Models.RaiderIO.Character.MythicPluses
{
    public class DefaultMythicPlus
    {
        public string Dungeon { get; set; }

        [JsonProperty(PropertyName = "short_name")]
        public string ShortName { get; set; }

        [JsonProperty(PropertyName = "mythic_level")]
        public int MythicLevel { get; set; }

        [JsonProperty(PropertyName = "completed_at")]
        public string CompletedAt { get; set; }

        [JsonProperty(PropertyName = "clear_time_ms")]
        public float ClearTimeMs { get; set; }

        [JsonProperty(PropertyName = "num_keystone_upgrades")]
        public float NumKeystoneUpgrades { get; set; }

        [JsonProperty(PropertyName = "map_challenge_mode_id")]
        public int MapChallangeModeId { get; set; }

        public float Score { get; set; }

        public Affixes [] Affixes { get; set; }

        public string Url { get; set; }
    }
}
