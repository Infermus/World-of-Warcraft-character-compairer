using System.Collections.Generic;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Mappers;

namespace WowCharComparerWebApp.Logic.HeartOfAzeroth
{
    public class HeartOfAzerothComparer
    {
        public static CharacterHeartOfAzerothCompareResult CompareHeartOfAzerothLevel(List<CharacterModel> parsedResultList)
        {
            float hoALevelCountResult = parsedResultList[0].Items.Neck.AzeriteItem.AzeriteLevel > parsedResultList[1].Items.Neck.AzeriteItem.AzeriteLevel
                                                   ? parsedResultList[0].Items.Neck.AzeriteItem.AzeriteLevel - parsedResultList[1].Items.Neck.AzeriteItem.AzeriteLevel
                                                   : parsedResultList[1].Items.Neck.AzeriteItem.AzeriteLevel - parsedResultList[0].Items.Neck.AzeriteItem.AzeriteLevel;

            return new CharacterHeartOfAzerothCompareResult()
            {
                HeartOfAzerothLevelDifferance = hoALevelCountResult
            };
        }
    }
}
