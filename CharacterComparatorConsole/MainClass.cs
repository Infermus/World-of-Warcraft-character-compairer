using CharacterComparatorConsole.MathLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using WowCharComparerWebApp.Enums.BlizzardAPIFields;
using WowCharComparerWebApp.Models;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Servers;
using WowCharComparerWebApp.Models.Statistics;

namespace CharacterComparatorConsole
{
    class MainClass
    {
        static void Main(string[] args)
        {
            //    RequestLocalization requestLocalization = new RequestLocalization()
            //    {
            //        CoreRegionUrlAddress = WowCharComparerWebApp.Configuration.APIConf.BlizzardAPIWowEUAddress,
            //        Realm = new Realm() { Slug = "burning-legion", Locale = "en_GB" }
            //    };

            //    List<string> characterNamesToCompare = new List<string>
            //    {
            //        "Selectus",
            //        "Wykminiacz"
            //    };

            //    List<CharacterModel> parsedResultList = new List<CharacterModel>();
            //    foreach (string name in characterNamesToCompare)
            //    {
            //        var result = WowCharComparerWebApp.Data.RequestsRepository.GetCharacterDataAsJsonAsync(name,
            //                                                                    requestLocalization,
            //                                                                    new List<CharacterFields>()
            //                                                                    {
            //                                                                    CharacterFields.Appearance
            //                                                                    }).Result;

            //        CharacterModel parsedResult = WowCharComparerWebApp.Data.Helpers.ResponseResultFormater.DeserializeJsonData<CharacterModel>(result.Data);
            //        parsedResultList.Add(parsedResult);
            //    }

            //    List<KeyValuePair<int, int>> primaryStatsList = new List<KeyValuePair<int, int>>()
            //    {
            //        new KeyValuePair<int, int>(parsedResultList[0].Stats.Str,parsedResultList[1].Stats.Str),
            //        new KeyValuePair<int, int>(parsedResultList[0].Stats.Int,parsedResultList[1].Stats.Int),
            //        new KeyValuePair<int, int>(parsedResultList[0].Stats.Agi,parsedResultList[1].Stats.Agi),
            //        new KeyValuePair<int, int>(parsedResultList[0].Stats.Sta,parsedResultList[1].Stats.Sta)
            //    };

            //    var countedPrimaryStatsPercent = StatsComparer.ComparePrimaryCharacterStats(parsedResultList);
            //    List<KeyValuePair<MathLogic.Stats, string>> finalResultList = new List<KeyValuePair<MathLogic.Stats, string>>();

            //    List<MathLogic.Stats> countedPrimaryStatsPercentKeys = (from list in countedPrimaryStatsPercent
            //                                                            select list.Key).ToList();

            //    for (int index = 0; index < countedPrimaryStatsPercent.Count; index++)
            //    {
            //        string result = primaryStatsList[index].Key > primaryStatsList[index].Value ? MainStatsPercentFormater.AddPlusToPrimaryStatPercent(countedPrimaryStatsPercent[index].Value.ToString())
            //                                                                                    : MainStatsPercentFormater.AddMinusToPrimaryStatPercent(countedPrimaryStatsPercent[index].Value.ToString());

            //        finalResultList.Add(new KeyValuePair<MathLogic.Stats, string>(countedPrimaryStatsPercentKeys[index], result));
            //    }

            Dictionary<int, string> statisticDictionary = new Dictionary<int, string>();
            Statistics JsonData = JsonProcessing.GetDataFromJsonFile<Statistics>(@"\Statistics.json");

            for (int index = 0; index < JsonData.BonusStats.Length; index++)
            {
                statisticDictionary.Add(JsonData.BonusStats[index].Id, JsonData.BonusStats[index].Name);
                //TODO There is a problem with names, versatility peforming two times 
                //Check if both are needed, indexes: 35,62
            }

            Console.WriteLine(statisticDictionary[7]);
           // return finalResultList;
        }      
    }
}
