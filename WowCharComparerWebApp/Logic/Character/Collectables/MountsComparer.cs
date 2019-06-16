using System;
using System.Collections.Generic;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Mappers;

namespace WowCharComparerWebApp.Logic.Character.Collectables
{
    public class MountsComparer
    {
        public static CharacterMountsCompareResult CompareMounts(List<ExtendedCharacterModel> parsedResultList)
        {
            int mountCollectedResult = Math.Abs(parsedResultList[0].Mounts.NumberOfCollectedMounts - parsedResultList[1].Mounts.NumberOfCollectedMounts);
            int mountNotCollectedResult = Math.Abs(parsedResultList[0].Mounts.NumberOfNotCollectedMounts - parsedResultList[1].Mounts.NumberOfNotCollectedMounts);

            return new CharacterMountsCompareResult()
            {
                CollectedMountifferance = mountCollectedResult,
                NotCollectedMountDifferance = mountNotCollectedResult
            };
        }
    }
}
