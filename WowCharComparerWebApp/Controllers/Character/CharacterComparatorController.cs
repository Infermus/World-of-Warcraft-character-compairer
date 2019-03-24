using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WowCharComparerWebApp.Configuration;
using WowCharComparerWebApp.Data.ApiRequests;
using WowCharComparerWebApp.Data.Database.Repository;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Enums;
using WowCharComparerWebApp.Enums.Locale;
using WowCharComparerWebApp.Enums.RaiderIO;
using WowCharComparerWebApp.Models.Achievement;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.RaiderIO.Character;
using WowCharComparerWebApp.Models.Servers;

namespace WowCharComparerWebApp.Controllers.CharacterControllers
{
    public class CharacterComparatorController : Controller
    {
        private static List<string> currentRealmListPlayerLeft;
        private static List<string> currentRealmListPlayerRight;


        public IActionResult Index()
        {
            //Note: Local variable to to avoid 2x requests for both players at default;
            List<string> defaultRegionForBothPlayer = GetRealmListByRegion(Region.Europe);

            currentRealmListPlayerLeft = defaultRegionForBothPlayer;
            currentRealmListPlayerRight = defaultRegionForBothPlayer;

            ViewData["realmsListLeftPlayer"] = currentRealmListPlayerLeft;
            ViewData["realmsListRightPlayer"] = currentRealmListPlayerRight;

            return View();
        }

        #region Selecting region actions

        [Route("selected-region-left")]
        public IActionResult SelectedRegionLeftPlayer(Region region)
        {
            currentRealmListPlayerLeft = GetRealmListByRegion(region);

            ViewData["realmsListLeftPlayer"] = currentRealmListPlayerLeft;
            ViewData["realmsListRightPlayer"] = currentRealmListPlayerRight;

            return View("Index");
        }

        [Route("selected-region-right")]
        public IActionResult SelectedRegionRightPlayer(Region region)
        {
            currentRealmListPlayerRight = GetRealmListByRegion(region);

            ViewData["realmsListLeftPlayer"] = currentRealmListPlayerLeft;
            ViewData["realmsListRightPlayer"] = currentRealmListPlayerRight;

            return View("Index");
        }

        #endregion

        public IActionResult TestActionOne()
        {
            List<Character> parsedResultList = new List<Character>();

            RequestLocalization requestLocalization = new RequestLocalization()
            {
                CoreRegionUrlAddress = APIConf.RaiderIOAdress, // refactor this
                Realm = new Realm() { Slug = "burning-legion", Locale = "en_GB", Timezone = "Europe/Paris" }
            };

            List<string> characterNamesToCompare = new List<string>
            {
                    "Wykminiacz",
                    "Selectus"
            };

            foreach (string name in characterNamesToCompare)
            {
                var result = RaiderIORequests.GetRaiderIODataAsync(name, 
                                                                    requestLocalization, new List<RaiderIOCharacterFields>
                                                                    {
                                                                        RaiderIOCharacterFields.MythicPlusBestRuns,
                                                                        RaiderIOCharacterFields.MythicPlusRanks
                                                                    }).Result;

                Character parsedResult = JsonProcessing.DeserializeJsonData<Character>(result.Data);
                parsedResultList.Add(parsedResult);
            }
            #region Testing character compare result
            //List<CharacterModel> parsedResultList = new List<CharacterModel>();

            //foreach (string name in characterNamesToCompare)
            //{
            //    var result = CharacterRequests.GetCharacterDataAsJsonAsync(name,
            //                                                                requestLocalization,
            //                                                                new List<CharacterFields>()
            //                                                                {
            //                                                                    CharacterFields.Items
            //                                                                }).Result;

             //   CharacterModel parsedResult = JsonProcessing.DeserializeJsonData<CharacterModel>(result.Data);
            //    parsedResultList.Add(parsedResult);
            //}
            #endregion

            return StatusCode(200);
        }

        public IActionResult TestActionTwo()
        {
            temp_DataPreparation.InsertBonusStatsTableFromJsonFile();

            return Content("Action two - executed");
        }

        public IActionResult TestActionThree()
        {
            string returnContent = string.Empty;

            var data = DataResources.GetCharacterAchievements(new RequestLocalization()
            {
                CoreRegionUrlAddress = APIConf.BlizzadAPIAddressWrapper[Region.Europe],
                Realm = new Realm() { Locale = EULocale.en_GB.ToString() }
            });

            if (data.Result.Exception is null)
            {
                Achievement parsedResult = JsonProcessing.DeserializeJsonData<Achievement>(data.Result.Data);
                temp_DataPreparation.InsertAllAchievementsDataFromApiRequest(parsedResult);

                returnContent = "Test action three executed - achievements insertion to database";
            }
            else
            {
                returnContent = data.Result.Exception.Message;
            }
            
            return Content(returnContent);
        }

        public IActionResult ComparePlayers(CharacterModel firstPlayer, CharacterModel secondPlayer)
        {
            //string realm = Request.Form["Realm"].ToString();

            //RequestLocalization requestLocalization = new RequestLocalization()
            //{
            //    CoreRegionUrlAddress = APIConf.BlizzardAPIWowEUAddress,
            //    Realm = new Realm()
            //    {
            //        Slug = realm,
            //        Locale = "en_GB"
            //    }
            //};

            //BlizzardAPIResponse characterResponse = RequestsRepository.GetCharacterDataAsJsonAsync(firstNickToCompare, requestLocalization, new System.Collections.Generic.List<WowCharComparerLib.Enums.BlizzardAPIFields.CharacterFields>()).Result;

            return View();
        }


        private List<string> GetRealmListByRegion(Region region)
        {
            List<string> realmsNames = new List<string>();
            string regionCoreAdress = string.Empty;

            try
            {
                if (APIConf.BlizzadAPIAddressWrapper.TryGetValue(region, out regionCoreAdress) == false)
                {
                    throw new KeyNotFoundException($"Cannot find region in {APIConf.BlizzadAPIAddressWrapper.GetType().Name} dictionary");
                }

                RequestLocalization requestLocalization = new RequestLocalization()
                {
                    CoreRegionUrlAddress = regionCoreAdress,
                };

                RealmsRequests realmsRequests = new RealmsRequests();
                var realmResponse = realmsRequests.GetRealmsDataAsJsonAsync(requestLocalization);
                RealmStatus realmStatus = JsonProcessing.DeserializeJsonData<RealmStatus>(realmResponse.Result.Data);

                foreach (Realm realmsData in realmStatus.Realms)
                {
                    realmsNames.Add(realmsData.Name);
                }
            }
            catch (Exception)
            {
                realmsNames = new List<string>();
            }

            return realmsNames;
        }
    }
}