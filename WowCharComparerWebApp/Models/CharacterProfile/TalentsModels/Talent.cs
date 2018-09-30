
namespace WowCharComparerWebApp.Models.CharacterProfile.TalentsModels
{
    public class Talent
    {
        public int Tier { get; set; }

        public int Column { get; set; }

        public Spell Spell { get; set; }

        public Spec Spec { get; set; }
    }
}
