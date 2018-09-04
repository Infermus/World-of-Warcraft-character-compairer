using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowCharComparerLib.Enums.BlizzardAPIFields;

namespace WowCharComparerLib.Enums
{
    public static class EnumDictonaryWrapper
    {
        public static Dictionary<CharacterFields, string> characterFieldsDicWrapper = new Dictionary<CharacterFields, string>()
        {
            { CharacterFields.Achievements, "achievements" },
            { CharacterFields.Appearance, "appearance" },
            { CharacterFields.Items, "items" },
            { CharacterFields.Guild, "guild" },
            { CharacterFields.PetSlots, "petSlots" },
            //TODO add rest;

            { CharacterFields.HunterPets, "hunterPets" },
        };

    }
}
