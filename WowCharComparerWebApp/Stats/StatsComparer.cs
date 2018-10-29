using System;
using System.Collections.Generic;
using WowCharComparerWebApp.Models.CharacterProfile;

namespace WowCharComparerWebApp.Stats
{
    internal class StatsComparer
    {
        public static List<KeyValuePair<Enums.Stats, decimal>> ComparePrimaryCharacterStats(List<CharacterModel> parsedResultList)
        {
            List<KeyValuePair<Enums.Stats, decimal>> countedPrimaryStatsPercent = new List<KeyValuePair<Enums.Stats, decimal>>();

            try
            {
                if (parsedResultList.Count == 2)
                {
                    List<Tuple<Enums.Stats, int, int>> minMaxPrimaryStatsTuple = new List<Tuple<Enums.Stats, int, int>>
                    {
                        new Tuple<Enums.Stats, int, int>(Enums.Stats.Str,
                                                    Math.Max(parsedResultList[0].Stats.Str, parsedResultList[1].Stats.Str),
                                                    Math.Min(parsedResultList[0].Stats.Str, parsedResultList[1].Stats.Str)),
                        new Tuple<Enums.Stats, int, int>(Enums.Stats.Int,
                                                    Math.Max(parsedResultList[0].Stats.Int, parsedResultList[1].Stats.Int),
                                                    Math.Min(parsedResultList[0].Stats.Int, parsedResultList[1].Stats.Int)),
                        new Tuple<Enums.Stats, int, int>(Enums.Stats.Agi,
                                                    Math.Max(parsedResultList[0].Stats.Agi, parsedResultList[1].Stats.Agi),
                                                    Math.Min(parsedResultList[0].Stats.Agi, parsedResultList[1].Stats.Agi)),
                        new Tuple<Enums.Stats, int, int>(Enums.Stats.Sta,
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

        public static List<KeyValuePair<Enums.Stats, decimal>> PrimaryStatsPercentCalculation(List<Tuple<Enums.Stats, int, int>> primaryStats)
        {
            List<KeyValuePair<Enums.Stats, decimal>> countedPrimaryStatsPercent = new List<KeyValuePair<Enums.Stats, decimal>>();

            for (int index = 0; index <= 3; index++)
            {
                decimal value1 = primaryStats[index].Item2;
                decimal value2 = primaryStats[index].Item3;
                decimal countedPercent = decimal.Round(((value1 - value2) / value2) * 100, 2);
                countedPrimaryStatsPercent.Add(new KeyValuePair<Enums.Stats, decimal>(primaryStats[index].Item1, countedPercent));
            }
            return countedPrimaryStatsPercent;
        }       
    }
}
