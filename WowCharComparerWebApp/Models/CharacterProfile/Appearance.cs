
namespace WowCharComparerWebApp.Models.CharacterProfile
{
    public class Appearance
    {
        public int FaceVariation { get; set; }

        public int SkinColor { get; set; }

        public int HairColor { get; set; }

        public int HairVariation { get; set; }

        public int FeatureVariation { get; set; }

        public bool ShowHelmet { get; set; }

        public bool ShowCloak { get; set; }

        public int [] CustomDisplayOptions { get; set; }
    }
}