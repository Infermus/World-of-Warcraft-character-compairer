using WowCharComparerWebApp.Models.CharacterProfile;

namespace WowCharComparerWebApp.ViewModel.CharacterProfile
{
    public class BasicCharacterViewModel
    {
        public string Name { get; set; }

        public string Race { get; set; }

        public int Level { get; set; }

        public string Class { get; set; }

        public int AchievementPoints { get; set; }

        public string Thumbnail { get; set; }

        public string Faction { get; set; }

        public int TotalHonorableKills { get; set; }
    }
}
