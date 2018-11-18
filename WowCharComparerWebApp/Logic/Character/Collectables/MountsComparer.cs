using System.Collections.Generic;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Mappers;

namespace WowCharComparerWebApp.Logic.Character.Collectables
{
    public class MountsComparer
    {
        public static CharacterMountsCompareResult CompareMounts(List<CharacterModel> parsedResultList)
        {
            int mountCollectedResult = parsedResultList[0].Mounts.NumberOfCollectedMounts > parsedResultList[1].Mounts.NumberOfCollectedMounts
                                       ? parsedResultList[0].Mounts.NumberOfCollectedMounts - parsedResultList[1].Mounts.NumberOfCollectedMounts
                                       : parsedResultList[1].Mounts.NumberOfCollectedMounts - parsedResultList[0].Mounts.NumberOfCollectedMounts;

            int mountNotCollectedResult = parsedResultList[0].Mounts.NumberOfNotCollectedMounts > parsedResultList[1].Mounts.NumberOfNotCollectedMounts
                                       ? parsedResultList[0].Mounts.NumberOfNotCollectedMounts - parsedResultList[1].Mounts.NumberOfNotCollectedMounts
                                       : parsedResultList[1].Mounts.NumberOfNotCollectedMounts - parsedResultList[0].Mounts.NumberOfNotCollectedMounts;

            return new CharacterMountsCompareResult()
            {
                CollectedMountifferance = mountCollectedResult,
                NotCollectedMountDifferance = mountNotCollectedResult
            };
        }
    }
}
