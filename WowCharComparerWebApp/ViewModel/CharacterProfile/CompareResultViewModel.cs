using System.Collections.Generic;
using WowCharComparerWebApp.Models.Mappers;

namespace WowCharComparerWebApp.ViewModel.CharacterProfile
{
    public class CompareResultViewModel
    {
        public List<ProcessedCharacterViewModel> ProcessedCharacterViewModel { get; set; }

        public CharacterAchievementPointsCompareResult AchievementPointsCompareResult { get; set; }

        public CharacterArenaRatingsCompareResult ArenaRatingsCompareResult { get; set; }

        public CharacterHeartOfAzerothCompareResult HeartOfAzerothCompareResult { get; set; }

        public CharacterHonorableKillsCompareResult HonorableKillsCompareResult { get; set; }

        public CharacterItemLevelCompareResult ItemLevelCompareResult { get; set; }

        public CharacterMiniPetsCompareResult MiniPetsCompareResult { get; set; }

        public CharacterMountsCompareResult MountsCompareResult { get; set; }

        public CharacterStatisticsCompareResult StatisticsCompareResult { get; set; }
    }
}
