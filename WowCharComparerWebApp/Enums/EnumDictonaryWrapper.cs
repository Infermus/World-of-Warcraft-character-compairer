using System.Collections.Generic;
using WowCharComparerWebApp.Enums.BlizzardAPIFields;

namespace WowCharComparerWebApp.Enums
{
    public static class EnumDictonaryWrapper
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
    }
}