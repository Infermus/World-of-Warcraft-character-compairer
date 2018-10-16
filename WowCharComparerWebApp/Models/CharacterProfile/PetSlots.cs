
using System;

namespace WowCharComparerWebApp.Models.CharacterProfile
{
    public class PetSlots
    {
        public int Slot { get; set; }

        public string BattlePetGuid { get; set;  }

        public bool IsEmpty { get; set; }

        public bool IsLocked { get; set; }

        public short [] Abilities { get; set; }
    }
}