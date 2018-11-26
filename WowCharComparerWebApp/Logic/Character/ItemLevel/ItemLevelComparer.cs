using System;
using System.Collections.Generic;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Mappers;

namespace WowCharComparerWebApp.Logic.ItemLevel
{
    public class ItemLevelComparer
    {
        public static CharacterItemLevelCompareResult CompareCharactersItemLevel(List<CharacterModel> parsedResultList)
        {
            float equippedResult = Math.Abs(parsedResultList[0].Items.AverageItemLevelEquipped - parsedResultList[1].Items.AverageItemLevelEquipped);

            float notEquippedResult = Math.Abs(parsedResultList[0].Items.AverageItemLevel - parsedResultList[1].Items.AverageItemLevelEquipped);

            return new CharacterItemLevelCompareResult()
            {
                AverageItemLevelEquippedDifferance = equippedResult,
                AverageItemLevelNotEquippedDifferance = notEquippedResult
            };
        }
    }
}