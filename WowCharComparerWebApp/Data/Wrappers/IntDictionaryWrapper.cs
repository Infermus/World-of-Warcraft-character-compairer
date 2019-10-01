using System.Collections.Generic;

namespace WowCharComparerWebApp.Data.Wrappers
{
    public static class IntDictionaryWrapper
    {
        public static Dictionary<int, string> characterClassWrapper = new Dictionary<int, string>
        {
            [1] = "Warrior",
            [2] = "Paladin",
            [3] = "Hunter",
            [4] = "Rogue",
            [5] = "Priest",
            [6] = "Death Knight",
            [7] = "Shaman",
            [8] = "Mage",
            [9] = "Warlock",
            [10] = "Monk",
            [11] = "Druid",
            [12] = "Demon Hunter"
        };

        public static Dictionary<int, string> characterRaceWrapper = new Dictionary<int, string>
        {
            [1] = "Human",
            [2] = "Orc",
            [3] = "Dwarf",
            [4] = "Night Elf",
            [5] = "Undead",
            [6] = "Tauren",
            [7] = "Gnome",
            [8] = "Troll",
            [9] = "Goblin",
            [10] = "Blood Elf",
            [11] = "Draenei",
            [12] = "Fel Orc",
            [13] = "Naga",
            [14] = "Broken",
            [15] = "Skeleton",
            [16] = "Vrykul",
            [17] = "Tuskarr",
            [18] = "Forest Troll",
            [19] = "Taunka",
            [20] = "Northrend Skeleton",
            [21] = "Ice Troll",
            [22] = "Worgen",
            [23] = "Gilnean",
            [24] = "Pandaren", // Neutral
            [25] = "Pandaren", // Alliance
            [26] = "Pandaren", // Horde
            [27] = "Nightborne",
            [28] = "HighmountainTauren",
            [29] = "Void Elf",
            [30] = "Lightforged Draenei",
            [31] = "Zandalari Troll",
            [32] = "Kul Tiran",
            [33] = "Human", // Thin Human
            [34] = "Dark Iron Dwarf",
            [35] = "Vulpera",
            [36] = "Mag'har Orc",
            [37] = "Mechagnome"
        };
    }
}
