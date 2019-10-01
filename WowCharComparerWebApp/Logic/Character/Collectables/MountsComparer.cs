using System;
using System.Collections.Generic;
using System.Linq;
using WowCharComparerWebApp.Models.Mappers;
using WowCharComparerWebApp.ViewModel.CharacterProfile;

namespace WowCharComparerWebApp.Logic.Character.Collectables
{
    public class MountsComparer
    {
        public CharacterMountsCompareResult CompareMounts(List<ProcessedCharacterViewModel> parsedResultList)
        {
            var result = new CharacterMountsCompareResult();

            if (parsedResultList.Any(x => x.Mounts != null))
            {
                result.CollectedMountifferance = Math.Abs(parsedResultList[0].Mounts.NumberOfCollectedMounts - parsedResultList[1].Mounts.NumberOfCollectedMounts);
                result.NotCollectedMountDifferance = Math.Abs(parsedResultList[0].Mounts.NumberOfNotCollectedMounts - parsedResultList[1].Mounts.NumberOfNotCollectedMounts);
            }

            return result;
        }
    }
}
