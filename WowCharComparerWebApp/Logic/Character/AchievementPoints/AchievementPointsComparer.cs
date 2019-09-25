using System;
using System.Collections.Generic;
using WowCharComparerWebApp.Models.Mappers;
using WowCharComparerWebApp.ViewModel.CharacterProfile;

namespace WowCharComparerWebApp.Logic.Character.AchievementPoints
{
    public class AchievementPointsComparer
    {
        public CharacterAchievementPointsCompareResult CompareAchievementPoints(List<ProcessedCharacterViewModel> parsedResultList)
        {
            int achievementPointResult = Math.Abs(parsedResultList[0].BasicCharacterData.AchievementPoints - parsedResultList[1].BasicCharacterData.AchievementPoints);

            return new CharacterAchievementPointsCompareResult()
            {
                AchievementPointsDifferance = achievementPointResult
            };
        }
    }
}
