using System.Collections.Generic;
using WowCharComparerWebApp.Models.Achievement;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.CharacterProfile.ItemsModels.Others;

namespace WowCharComparerWebApp.Models
{
    public class ProcessedCharacterModel 
    {
        public BasicCharacterModel RawCharacterData { get; set; }

        public IEnumerable<AchievementsData> AchievementsData { get; set; }

        public Items Items { get; set; }
    }
}