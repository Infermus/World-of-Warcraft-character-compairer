using System.Collections.Generic;
using WowCharComparerWebApp.Models.Mappers;
using System;
using WowCharComparerWebApp.ViewModel.CharacterProfile;
using System.Linq;

namespace WowCharComparerWebApp.Logic.Character.Pvp
{
    public class HonorableKillsComparer
    {
        public CharacterHonorableKillsCompareResult CompareHonorableKills(List<ProcessedCharacterViewModel> parsedResultList)
        {
            var result = new CharacterHonorableKillsCompareResult();

            if (parsedResultList.Any(x => x.BasicCharacterData != null))
            {
                result.HonorableKillResult = Math.Abs(parsedResultList[0].BasicCharacterData.TotalHonorableKills - parsedResultList[1].BasicCharacterData.TotalHonorableKills);
            }

            return result;
        }
    }
}
