using System;
using System.Collections.Generic;
using System.Linq;
using WowCharComparerWebApp.Models.Mappers;
using WowCharComparerWebApp.ViewModel.CharacterProfile;

namespace WowCharComparerWebApp.Logic.ItemLevel
{
    public class ItemLevelComparer
    {
        public CharacterItemLevelCompareResult CompareCharactersItemLevel(List<ProcessedCharacterViewModel> parsedResultList)
        {
            var result = new CharacterItemLevelCompareResult();

            if (parsedResultList.Any(x => x.Items != null))
            {
                result.AverageItemLevelEquippedDifferance = Math.Abs(parsedResultList[0].Items.AverageItemLevelEquipped - parsedResultList[1].Items.AverageItemLevelEquipped);
                result.AverageItemLevelNotEquippedDifferance = Math.Abs(parsedResultList[0].Items.AverageItemLevel - parsedResultList[1].Items.AverageItemLevel);
            }

            return result;
        }
    }
}