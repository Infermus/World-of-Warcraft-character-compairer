using System;
using System.Collections.Generic;
using WowCharComparerWebApp.Enums.Character;
using WowCharComparerWebApp.Models.CharacterProfile;

namespace WowCharComparerWebApp.Logic.Character.Statistics
{
    internal class StatsComparer
    {
        public static List<KeyValuePair<CharacterMainStats, decimal>> ComparePrimaryCharacterStats(List<CharacterModel> parsedResultList)
        {
            List<KeyValuePair<CharacterMainStats, decimal>> countedPrimaryStatsPercent = new List<KeyValuePair<CharacterMainStats, decimal>>();

            try
            {
                if (parsedResultList.Count == 2)
                {
                    List<Tuple<CharacterMainStats, int, int>> minMaxPrimaryStatsTuple = new List<Tuple<CharacterMainStats, int, int>>
                    {
                        new Tuple<CharacterMainStats, int, int>(CharacterMainStats.Str,
                                                    Math.Max(parsedResultList[0].Stats.Str, parsedResultList[1].Stats.Str),
                                                    Math.Min(parsedResultList[0].Stats.Str, parsedResultList[1].Stats.Str)),
                        new Tuple<CharacterMainStats, int, int>(CharacterMainStats.Int,
                                                    Math.Max(parsedResultList[0].Stats.Int, parsedResultList[1].Stats.Int),
                                                    Math.Min(parsedResultList[0].Stats.Int, parsedResultList[1].Stats.Int)),
                        new Tuple<CharacterMainStats, int, int>(CharacterMainStats.Agi,
                                                    Math.Max(parsedResultList[0].Stats.Agi, parsedResultList[1].Stats.Agi),
                                                    Math.Min(parsedResultList[0].Stats.Agi, parsedResultList[1].Stats.Agi)),
                        new Tuple<CharacterMainStats, int, int>(CharacterMainStats.Sta,
                                                    Math.Max(parsedResultList[0].Stats.Sta, parsedResultList[1].Stats.Sta),
                                                    Math.Min(parsedResultList[0].Stats.Sta, parsedResultList[1].Stats.Sta)),
                    };

                    countedPrimaryStatsPercent = PrimaryStatsPercentCalculation(minMaxPrimaryStatsTuple);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);               
            }

            return countedPrimaryStatsPercent;
        }

        private static List<KeyValuePair<CharacterMainStats, decimal>> PrimaryStatsPercentCalculation(List<Tuple<CharacterMainStats, int, int>> primaryStats)
        {
            List<KeyValuePair<CharacterMainStats, decimal>> countedPrimaryStatsPercent = new List<KeyValuePair<CharacterMainStats, decimal>>();

            for (int index = 0; index <= 3; index++)
            {
                decimal value1 = primaryStats[index].Item2;
                decimal value2 = primaryStats[index].Item3;
                decimal countedPercent = decimal.Round(((value1 - value2) / value2) * 100, 2);
                countedPrimaryStatsPercent.Add(new KeyValuePair<CharacterMainStats, decimal>(primaryStats[index].Item1, countedPercent));
            }
            return countedPrimaryStatsPercent;
        }       
    }
}
