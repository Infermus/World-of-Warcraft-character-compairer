using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Data.ApiRequests;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Enums.BlizzardAPIFields;
using WowCharComparerWebApp.Models;
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

            var achievementsResourcesData = DataResources.GetCharacterAchievements(requestLocalization);


            List<string> characterNamesToCompare = new List<string>
            {
                    "Selectus",
                    "Wykminiacz"
            };

            foreach (string name in characterNamesToCompare)
            {
                var result = CharacterRequests.GetCharacterDataAsJsonAsync(name,
                                                                            requestLocalization,
                                                                            new List<CharacterFields>()
                                                                            {
                                                                                CharacterFields.Stats
                                                                            }).Result;

                CharacterModel parsedResult = JsonProcessing.DeserializeJsonData<CharacterModel>(result.Data);
                parsedResultList.Add(parsedResult);
            }
            StatsOperations(parsedResultList);
            var parsedJsonData = JsonProcessing.GetDataFromJsonFile<Models.Statistics.Statistics>(@"\Statistics.json");

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                using (var db = new ComparerDatabaseContext())
                {
                    db.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT dbo.BonusStats ON");
                    for (int index = 0; index < parsedJsonData.BonusStats.Length; index++)
                    {
                        db.BonusStats.AddRange(new BonusStats()
                        {
                            Id = Guid.NewGuid(),
                            StatisticId = parsedJsonData.BonusStats[index].StatisticId,
                            Name = parsedJsonData.BonusStats[index].Name
                        });
                    }
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT dbo.BonusStats OFF");
                    scope.Complete();
                }
            }
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
        //Dictionary<int,string> jsonDataInDictionary = JsonProcessing.AddDataToDictionary(jsonData);

    }
}
