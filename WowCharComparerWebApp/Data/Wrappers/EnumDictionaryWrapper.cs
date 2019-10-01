using System.Collections.Generic;
using WowCharComparerWebApp.Enums;
using WowCharComparerWebApp.Enums.BlizzardAPIFields;
using WowCharComparerWebApp.Enums.RaiderIO;

namespace WowCharComparerWebApp.Data.Wrappers
{
    public static class EnumDictionaryWrapper
    {
        public static Dictionary<CharacterFields, string> characterFieldsWrapper = new Dictionary<CharacterFields, string>()
        {
            { CharacterFields.Achievements, "achievements" },
            { CharacterFields.Appearance, "appearance" },
            { CharacterFields.Feed, "feed" },
            { CharacterFields.Guild, "guild" },
            { CharacterFields.HunterPets, "hunterPets" },
            { CharacterFields.Items, "items" },
            { CharacterFields.Mounts, "mounts" },
            { CharacterFields.Pets, "pets" },
            { CharacterFields.PetSlots, "petSlots" },
            { CharacterFields.Professions, "professions" },
            { CharacterFields.Progression, "progression" },
            { CharacterFields.PVP, "pvp" },
            { CharacterFields.Quests, "quests" },
            { CharacterFields.Reputation, "reputation" },
            { CharacterFields.Statistics, "statistics" },
            { CharacterFields.Stats, "stats" },
            { CharacterFields.Talents, "talents" },
            { CharacterFields.Titles, "titles" },
            { CharacterFields.Audit, "audit" },
        };

        public static Dictionary<DataResourcesFields, string> dataResourcesFieldsWrapper = new Dictionary<DataResourcesFields, string>()
        {
            { DataResourcesFields.Battlegroups, "battlegroups" },
            { DataResourcesFields.CharacterRaces, "races" },
            { DataResourcesFields.CharacterClasses, "classes" },
            { DataResourcesFields.CharacterAchievements, "achievements" },
            { DataResourcesFields.GuildRewards, "rewards" },
            { DataResourcesFields.GuildPerks, "perks" },
            { DataResourcesFields.GuildAchievements, "achievements" },
            { DataResourcesFields.ItemClasses, "classes" },
            { DataResourcesFields.Talents, "talents" },
            { DataResourcesFields.PetTypes, "types" }
        };

        public static Dictionary<Region, string> regionsWrapper = new Dictionary<Region, string>()
        {
            { Region.Europe, "EU" },
            { Region.America, "US" },
            { Region.Korea, "KR" },
            { Region.Taiwan, "TW" }
        };

        public static Dictionary<RaiderIOCharacterFields, string> rioCharacterFieldWrapper = new Dictionary<RaiderIOCharacterFields, string>()
        {
            { RaiderIOCharacterFields.Gear, "gear" },
            { RaiderIOCharacterFields.Guild, "guild" },
            { RaiderIOCharacterFields.RaidProgression, "raid_progression" },
            { RaiderIOCharacterFields.MythicPlusScores, "mythic_plus_scores" },
            { RaiderIOCharacterFields.MythicPlusRanks, "mythic_plus_ranks" },
            { RaiderIOCharacterFields.MythicPlusBestRuns, "mythic_plus_best_runs" },
            { RaiderIOCharacterFields.MythicPlusHighestLevelRuns, "mythic_plus_highest_level_runs"},
            { RaiderIOCharacterFields.MythicPlusWeeklyHighestLevelRuns, "mythic_plus_weekly_highes_level_runs" },
            { RaiderIOCharacterFields.PreviousMythicPlusScores, "previous_mythic_plus_scores" },
            { RaiderIOCharacterFields.PreviousMythicPlusRanks, "previous_mythic_plus_ranks" },
            { RaiderIOCharacterFields.RaidAchievementMeta, "raid_achievement_meta" },
            { RaiderIOCharacterFields.RaidAchievementCurve, "raid_achievement_curve" }
        };

        #region From-view wrappers

        public static Dictionary<string, Region> viewRegionsWrapper = new Dictionary<string, Region>
        {
            { "Europe", Region.Europe },
            { "America", Region.America },
            { "Korea", Region.Korea },
            { "Taiwan", Region.Taiwan }
        };

        #endregion

    }
}