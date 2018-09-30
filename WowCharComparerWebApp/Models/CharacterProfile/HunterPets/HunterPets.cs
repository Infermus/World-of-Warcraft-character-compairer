
using Newtonsoft.Json;

namespace WowCharComparerWebApp.Models.CharacterProfile.HunterPets
{
    public class HunterPets
    {
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Creature")]
        public int CreatureId { get; set; }

        public bool Selected { get; set; }

        public int Slot { get; set; }

        public char CalcSpec { get; set; }

        public int FamilyId { get; set; }

        public string FamilyName { get; set; }

        public Spec Spec { get; set; }
    }
}
