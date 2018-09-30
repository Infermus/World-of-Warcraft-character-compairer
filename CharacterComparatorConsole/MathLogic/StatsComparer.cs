using System;
using System.Collections.Generic;
using WowCharComparerWebApp.Models;
using WowCharComparerWebApp.Models.CharacterProfile;

namespace CharacterComparatorConsole.MathLogic
{
    internal class StatsComparer
    {
        public static void ComparePrimaryCharacterStats(List<CharacterModel> parsedResultList)
        {
            try
            {
                if (parsedResultList.Count == 2)
                {
                    List<Tuple<Stats, int, int>> primaryStatsTuple = new List<Tuple<Stats, int, int>>
                    {
                        new Tuple<Stats, int, int>(Stats.Str,
                                                    Math.Max(parsedResultList[0].Stats.Str, parsedResultList[1].Stats.Str),
                                                    Math.Min(parsedResultList[0].Stats.Str, parsedResultList[1].Stats.Str)),
                        new Tuple<Stats, int, int>(Stats.Int,
                                                    Math.Max(parsedResultList[0].Stats.Int, parsedResultList[1].Stats.Int),
                                                    Math.Min(parsedResultList[0].Stats.Int, parsedResultList[1].Stats.Int)),
                        new Tuple<Stats, int, int>(Stats.Agi,
                                                    Math.Max(parsedResultList[0].Stats.Agi, parsedResultList[1].Stats.Agi),
                                                    Math.Min(parsedResultList[0].Stats.Agi, parsedResultList[1].Stats.Agi)),
                        new Tuple<Stats, int, int>(Stats.Sta,
                                                    Math.Max(parsedResultList[0].Stats.Sta, parsedResultList[1].Stats.Sta),
                                                    Math.Min(parsedResultList[0].Stats.Sta, parsedResultList[1].Stats.Sta)),
                    };
                    PrimaryStatsPercentCalculation(primaryStatsTuple);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static List<KeyValuePair<Stats, decimal>> PrimaryStatsPercentCalculation(List<Tuple<Stats, int, int>> primaryStats)
        {
            List<KeyValuePair<Stats, decimal>> CountedPrimaryStatsPercent = new List<KeyValuePair<Stats, decimal>>();

            for (int index = 0; index <= 3; index++)
            {
                decimal value1 = primaryStats[index].Item2;
                decimal value2 = primaryStats[index].Item3;
                decimal countedPercent = decimal.Round(((value1 - value2) / value2) * 100, 2);
                CountedPrimaryStatsPercent.Add(new KeyValuePair<Stats, decimal>(primaryStats[index].Item1, countedPercent));
            }
            return CountedPrimaryStatsPercent;
        }
    }
}
