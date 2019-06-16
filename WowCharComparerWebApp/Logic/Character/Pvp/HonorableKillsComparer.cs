using System.Collections.Generic;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Mappers;
using System;

namespace WowCharComparerWebApp.Logic.Character.Pvp
{
    public class HonorableKillsComparer
    {
        public static CharacterHonorableKillsCompareResult CompareHonorableKills(List<ExtendedCharacterModel> parsedResultList)
        {
            int killsResult = Math.Abs(parsedResultList[0].TotalHonorableKills - parsedResultList[0].TotalHonorableKills);

            return new CharacterHonorableKillsCompareResult()
            {
                HonorableKillResult = killsResult
            };
        }
    }
}
