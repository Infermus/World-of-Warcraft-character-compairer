
namespace WowCharComparerLib.Models.CharacterProfile.PetSlots
{
    public class PetSlots
    {
        public int Slot { get; set; }

        public string BattlePetGuid { get; set;  }

        public bool IsEmpty { get; set; }

        public bool IsLocked { get; set; }

        public Abilities [] Abilities { get; set; }
    }
}
