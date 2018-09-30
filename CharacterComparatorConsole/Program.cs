using CharacterComparatorConsole.MathLogic;
using System.Collections.Generic;
using WowCharComparerLib.APIConnection.Models;
using WowCharComparerLib.Enums.BlizzardAPIFields;
using WowCharComparerLib.Models;
using WowCharComparerLib.Models.Localization;

namespace CharacterComparatorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            RequestLocalization requestLocalization = new RequestLocalization()
            {
                CoreRegionUrlAddress = WowCharComparerLib.Configuration.APIConf.BlizzardAPIWowEUAddress,
                Realm = new Realm("burning-legion", "en_GB")
            };

            List<string> characterNamesToCompare = new List<string>
            {
                "Wykminiacz",
                "Apokalipsa"
            };

            List<CharacterModel> parsedResultList = new List<CharacterModel>();
            foreach (string name in characterNamesToCompare)
            {
                var result = WowCharComparerLib.APIConnection.RequestsRepository.GetCharacterDataAsJsonAsync(name,
                                                                            requestLocalization,
                                                                            new List<CharacterFields>()
                                                                            {
                                                                            CharacterFields.Stats
                                                                            }).Result;

                CharacterModel parsedResult = WowCharComparerLib.APIConnection.Helpers.ResponseResultFormater.DeserializeJsonData<CharacterModel>(result.Data);
                parsedResultList.Add(parsedResult);                                                       
            }
        
            StatsComparer.ComparePrimaryCharacterStats(parsedResultList);

        }
    }
}
