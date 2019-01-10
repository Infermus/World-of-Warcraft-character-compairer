using Newtonsoft.Json;
using WowCharComparerWebApp.Models.RaiderIO.Character.MythicPluses;

namespace WowCharComparerWebApp.Models.RaiderIO.Character
{
    public class Character
    {
        public string Name { get; set; }

        public string Race { get; set; }

        public string Class { get; set; }

        public string Active_Spec_Name { get; set; }

        public string Active_Spec_Role { get; set; }

        public string Gender { get; set; }

        public string Faction { get; set; }

        public string Region { get; set; }

        public string Realm { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Profile_Url { get; set; }

        [JsonProperty(PropertyName = "mythic_plus_scores")]
        public Mythic_Plus_Scores MythicPlusScores { get; set; }

        [JsonProperty(PropertyName = "mythic_plus_ranks")]
        public MythicPlusRanks.MythicPlusRanks MythicPlusRanks { get; set; }

        [JsonProperty(PropertyName = "mythic_plus_recent_runs")]
        public Mythic_Plus_Recent_Runs [] MythicPlusRecentRuns { get; set; }

        [JsonProperty(PropertyName = "mythic_plus_best_runs")]
        public Mythic_Plus_Best_Runs [] MythicPlusBestRuns { get; set; }

        [JsonProperty(PropertyName = "mythic_plus_highest_level_runs")]
        public Mythic_Plus_Highest_Level_Runs [] MythicPlusHighestLevelRuns{ get; set; }

        [JsonProperty(PropertyName = "mythic_plus_weekly_highest_level_runs")]
        public Mythic_Plus_Weekly_Highest_Level_Runs [] MythicPlusWeeklyHigestLevelRuns { get; set; }
    }
}
