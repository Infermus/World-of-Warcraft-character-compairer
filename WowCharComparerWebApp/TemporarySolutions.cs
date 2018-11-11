using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Data.ApiRequests;
using WowCharComparerWebApp.Enums.BlizzardAPIFields;
using WowCharComparerWebApp.Models;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Servers;

namespace WowCharComparerWebApp
{
    public static class TemporarySolutions
    {
        public static void SimpleCharacterCom()
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
        }


    }
}
