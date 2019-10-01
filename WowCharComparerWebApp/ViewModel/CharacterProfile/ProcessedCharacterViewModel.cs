using System.Collections.Generic;
using WowCharComparerWebApp.Models.Achievement;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.CharacterProfile.ItemsModels.Others;
using WowCharComparerWebApp.Models.CharacterProfile.MountsModels;
using WowCharComparerWebApp.Models.CharacterProfile.PetsModels;
using WowCharComparerWebApp.Models.CharacterProfile.ProgressionModels;
using WowCharComparerWebApp.Models.CharacterProfile.PvpModels;
using WowCharComparerWebApp.Models.CharacterProfile.StatisticsModels;
using WowCharComparerWebApp.Models.CharacterProfile.TalentsModels;

namespace WowCharComparerWebApp.ViewModel.CharacterProfile
{
    public class ProcessedCharacterViewModel
    {
        public BasicCharacterViewModel BasicCharacterData { get; set; }

        public Items Items { get; set; }

        public IEnumerable<AchievementsData> AchievementsData { get; set; }

        public Progression Progression { get; set; }

        public Pvp Pvp { get; set; }

        public Reputation[] Reputation { get; set; }

        public Statistics Statistics { get; set; }

        public Talents[] Talents { get; set; }

        public Pets Pets { get; set; }

        public Mounts Mounts { get; set; }

        public Models.CharacterProfile.Stats CharStats { get; set; }
    }
}
