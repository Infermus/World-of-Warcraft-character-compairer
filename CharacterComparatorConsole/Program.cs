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
                Realm = new Realm() {  Slug = "burning-legion", Locale = "en_GB"}
            };

            List<string> characterNamesToCompare = new List<string>
            {
                "Wykminiacz",
                "Apokalipsa"
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

            StatsComparer.ComparePrimaryCharacterStats(parsedResultList);

        }
    }
}
