﻿using System.Collections.Generic;
using WowCharComparerLib.Enums.BlizzardAPIFields;

namespace WowCharComparerLib.Enums
{
    public static class EnumDictonaryWrapper
    {
        public static Dictionary<CharacterFields, string> characterFieldsDicWrapper = new Dictionary<CharacterFields, string>()
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
    }
}