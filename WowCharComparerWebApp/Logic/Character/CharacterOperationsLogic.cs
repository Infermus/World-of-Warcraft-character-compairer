using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowCharComparerWebApp.Enums.Character;
using WowCharComparerWebApp.Logic.Character.Statistics;
using WowCharComparerWebApp.Models.CharacterProfile;

namespace WowCharComparerWebApp.Logic.Character
{
    public class CharacterOperationsLogic
    {
        public List<KeyValuePair<CharacterMainStats, string>> CompareCharacterStatistics(List<CharacterModel> parsedResultList)
        {
            List<KeyValuePair<CharacterMainStats, string>> finalResultList = new List<KeyValuePair<CharacterMainStats, string>>();
            try
            {
                List<KeyValuePair<int, int>> primaryStatsList = new List<KeyValuePair<int, int>>()
                {
                    new KeyValuePair<int, int>(parsedResultList[0].Stats.Str,parsedResultList[1].Stats.Str),
                    new KeyValuePair<int, int>(parsedResultList[0].Stats.Int,parsedResultList[1].Stats.Int),
                    new KeyValuePair<int, int>(parsedResultList[0].Stats.Agi,parsedResultList[1].Stats.Agi),
                    new KeyValuePair<int, int>(parsedResultList[0].Stats.Sta,parsedResultList[1].Stats.Sta)
                };

                var countedPrimaryStatsPercent = StatsComparer.ComparePrimaryCharacterStats(parsedResultList);

                List<CharacterMainStats> countedPrimaryStatsPercentKeys = (from list in countedPrimaryStatsPercent
                                                                          select list.Key).ToList();

                for (int index = 0; index < countedPrimaryStatsPercent.Count; index++)
                {
                    string result = primaryStatsList[index].Key > primaryStatsList[index].Value ? MainStatsPercentFormater.AddPlusToPrimaryStatPercent(countedPrimaryStatsPercent[index].Value.ToString())
                                                                                                : MainStatsPercentFormater.AddMinusToPrimaryStatPercent(countedPrimaryStatsPercent[index].Value.ToString());

                    finalResultList.Add(new KeyValuePair<CharacterMainStats, string>(countedPrimaryStatsPercentKeys[index], result));
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return finalResultList;
        }
    }
}
