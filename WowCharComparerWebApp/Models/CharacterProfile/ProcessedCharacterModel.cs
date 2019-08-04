using System.Collections.Generic;
using WowCharComparerWebApp.Models.Achievement;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.CharacterProfile.ItemsModels.Others;
using WowCharComparerWebApp.Models.CharacterProfile.ProgressionModels;
using WowCharComparerWebApp.Models.CharacterProfile.PvpModels;
using WowCharComparerWebApp.Models.CharacterProfile.TalentsModels;

namespace WowCharComparerWebApp.Models
{
    public class ProcessedCharacterModel 
    {
        public BasicCharacterModel RawCharacterData { get; set; }

        public Items Items { get; set; }

        public IEnumerable<AchievementsData> AchievementsData { get; set; }

        public Progression Progression { get; set; }

        public Pvp Pvp { get; set; }

        public Reputation[] Reputation { get; set; }

        public CharacterProfile.StatisticsModels.Statistics Statistics { get; set; }

        public Talents[] Talents { get; set; }
    }
}