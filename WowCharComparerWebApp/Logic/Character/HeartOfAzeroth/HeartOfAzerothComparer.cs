using System;
using System.Collections.Generic;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Mappers;

namespace WowCharComparerWebApp.Logic.HeartOfAzeroth
{
    public class HeartOfAzerothComparer
    {
        public static CharacterHeartOfAzerothCompareResult CompareHeartOfAzerothLevel(List<ExtendedCharacterModel> parsedResultList)
        {
            float hoALevelCountResult = Math.Abs(parsedResultList[0].Items.Neck.AzeriteItem.AzeriteLevel - parsedResultList[1].Items.Neck.AzeriteItem.AzeriteLevel);

            return new CharacterHeartOfAzerothCompareResult()
            {
                HeartOfAzerothLevelDifferance = hoALevelCountResult
            };
        }
    }
}
