using System;
using System.Collections.Generic;
using System.Linq;
using WowCharComparerWebApp.Models.Mappers;
using WowCharComparerWebApp.ViewModel.CharacterProfile;

namespace WowCharComparerWebApp.Logic.HeartOfAzeroth
{
    public class HeartOfAzerothComparer
    {
        public CharacterHeartOfAzerothCompareResult CompareHeartOfAzerothLevel(List<ProcessedCharacterViewModel> parsedResultList)
        {
            var result = new CharacterHeartOfAzerothCompareResult();

            if (parsedResultList.Any(x => x.Items.Neck != null) && parsedResultList.Any(x => x.Items.Neck.AzeriteItem != null))
            {
                result.HeartOfAzerothLevelDifferance = Math.Abs(parsedResultList[0].Items.Neck.AzeriteItem.AzeriteLevel - parsedResultList[1].Items.Neck.AzeriteItem.AzeriteLevel);
            }

            return result;
        }
    }
}
