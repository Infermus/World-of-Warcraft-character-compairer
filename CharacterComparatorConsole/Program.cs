using CharacterComparatorConsole.MathLogic;
using System.Collections.Generic;
using WowCharComparerWebApp.Enums.BlizzardAPIFields;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Servers;

namespace CharacterComparatorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            RequestLocalization requestLocalization = new RequestLocalization()
            {
                CoreRegionUrlAddress = WowCharComparerWebApp.Configuration.APIConf.BlizzardAPIWowEUAddress,
                Realm = new Realm() { Slug = "burning-legion", Locale = "en_GB" }
            };

            List<string> characterNamesToCompare = new List<string>
            {
                "Selectus",
                "Wykminiacz"
            };

            List<CharacterModel> parsedResultList = new List<CharacterModel>();
            foreach (string name in characterNamesToCompare)
            {
                var result = WowCharComparerWebApp.Data.RequestsRepository.GetCharacterDataAsJsonAsync(name,
                                                                            requestLocalization,
                                                                            new List<CharacterFields>()
                                                                            {
                                                                            CharacterFields.Stats
                                                                            }).Result;

                CharacterModel parsedResult = WowCharComparerWebApp.Data.Helpers.ResponseResultFormater.DeserializeJsonData<CharacterModel>(result.Data);
                parsedResultList.Add(parsedResult);
            }

            List<KeyValuePair<int, int>> primaryStatsList = new List<KeyValuePair<int, int>>()
            {
                new KeyValuePair<int, int>(parsedResultList[0].Stats.Str,parsedResultList[1].Stats.Str),
                new KeyValuePair<int, int>(parsedResultList[0].Stats.Int,parsedResultList[1].Stats.Int),
                new KeyValuePair<int, int>(parsedResultList[0].Stats.Agi,parsedResultList[1].Stats.Agi),
                new KeyValuePair<int, int>(parsedResultList[0].Stats.Sta,parsedResultList[1].Stats.Sta)
            };

            var countedPrimaryStatsPercent = StatsComparer.ComparePrimaryCharacterStats(parsedResultList);
            List<string> finalResultList = new List<string>();

            for (int index = 0; index < 4; index++)
            {
                var result = primaryStatsList[index].Key > primaryStatsList[index].Value ?
                 Adders.AddPlusToPrimaryStatPercent(countedPrimaryStatsPercent[index].Value.ToString()) :
                 Adders.AddMinusToPrimaryStatPercent(countedPrimaryStatsPercent[index].Value.ToString());

                finalResultList.Add(result);
            }
            //return finalResultList;
        }
    }
}
