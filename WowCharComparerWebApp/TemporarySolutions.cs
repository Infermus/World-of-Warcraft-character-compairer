using System;
using System.Collections.Generic;
using System.Linq;
using WowCharComparerWebApp.Enums.BlizzardAPIFields;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Servers;
using WowCharComparerWebApp.Stats;

namespace WowCharComparerWebApp
{
    public static class TemporarySolutions
    {
        public static void Request()
        {
            List<CharacterModel> parsedResultList = new List<CharacterModel>();
            RequestLocalization requestLocalization = new RequestLocalization()
            {
                CoreRegionUrlAddress = Configuration.APIConf.BlizzardAPIWowEUAddress,
                Realm = new Realm() { Slug = "burning-legion", Locale = "en_GB" }
            };

            List<string> characterNamesToCompare = new List<string>
            {
                    "Selectus",
                    "Wykminiacz"
            };

            foreach (string name in characterNamesToCompare)
            {
                var result = Data.RequestsRepository.GetCharacterDataAsJsonAsync(name,
                                                                            requestLocalization,
                                                                            new List<CharacterFields>()
                                                                            {
                                                                                CharacterFields.Stats
                                                                            }).Result;

                CharacterModel parsedResult = Data.Helpers.ResponseResultFormater.DeserializeJsonData<CharacterModel>(result.Data);
                parsedResultList.Add(parsedResult);
            }
           StatsOperations(parsedResultList);
        }

        public static List<KeyValuePair<Stats.Enums.Stats, string>> StatsOperations(List<CharacterModel> parsedResultList)
        {
            List<KeyValuePair<Stats.Enums.Stats, string>> finalResultList = new List<KeyValuePair<Stats.Enums.Stats, string>>();
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

                List<Stats.Enums.Stats> countedPrimaryStatsPercentKeys = (from list in countedPrimaryStatsPercent
                                                              select list.Key).ToList();

                for (int index = 0; index < countedPrimaryStatsPercent.Count; index++)
                {
                    string result = primaryStatsList[index].Key > primaryStatsList[index].Value ? MainStatsPercentFormater.AddPlusToPrimaryStatPercent(countedPrimaryStatsPercent[index].Value.ToString())
                                                                                                : MainStatsPercentFormater.AddMinusToPrimaryStatPercent(countedPrimaryStatsPercent[index].Value.ToString());

                    finalResultList.Add(new KeyValuePair<Stats.Enums.Stats, string>(countedPrimaryStatsPercentKeys[index], result));
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return finalResultList;
        }

        // ------------------------------------------------------------------------
        // Getting data from Json file
        //var jsonData = JsonProcessing.GetDataFromJsonFile<Models.Achievement.Achievement>(@"\AchievementData.json");

        //Dictionary<int,string> jsonDataInDictionary = JsonProcessing.AddDataToDictionary(jsonData);


    }
}
