using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WowCharComparerWebApp.Configuration;
using WowCharComparerWebApp.Data.ApiRequests;
using WowCharComparerWebApp.Data.Database.Repository;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Enums;
using WowCharComparerWebApp.Enums.BlizzardAPIFields;
using WowCharComparerWebApp.Enums.Locale;
using WowCharComparerWebApp.Logic.DataResources;
using WowCharComparerWebApp.Models;
using WowCharComparerWebApp.Models.Achievement;
using WowCharComparerWebApp.Models.CharacterProfile;
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult TestActionOne()
        {
            RequestLocalization requestLocalization = new RequestLocalization()
            {
                CoreRegionUrlAddress = APIConf.BlizzadAPIAddressWrapper[Region.Europe], // refactor this
                Realm = new Realm() { Slug = "burning-legion", Locale = "en_GB", Timezone = "Europe/Paris" }
            };

            List<string> characterNamesToCompare = new List<string>
            {
                    "Wykminiacz",
                    "Selectus"
            };

            #region Testing character compare result

            List<ExtendedCharacterModel> charactersToCompare = new List<ExtendedCharacterModel>();
            List<AchievementsData> matchedAchievementData = new List<AchievementsData>();

            List<ProcessedCharacterModel> processedCharacterData = new List<ProcessedCharacterModel>();

            foreach (string name in characterNamesToCompare)
            {
                var result = CharacterRequests.GetCharacterDataAsJsonAsync(name, requestLocalization,
                                                                            new List<CharacterFields>()
                                                                            {
                                                                                CharacterFields.Items,
                                                                                CharacterFields.Achievements
                                                                            }).Result;

                ExtendedCharacterModel currentCharacter = JsonProcessing.DeserializeJsonData<ExtendedCharacterModel>(result.Data);
                charactersToCompare.Add(currentCharacter);

                CharacterExtendedDataManager.MatchItemsBonusStatistics(charactersToCompare.Find(x => x.Name.Equals(name)));

                if (currentCharacter.Achievements != null)
                {
                    processedCharacterData.Add(new ProcessedCharacterModel()
                    {
                        RawCharacterData = new BasicCharacterModel
                        {
                            LastModified = currentCharacter.LastModified,
                            Name = currentCharacter.Name,
                            Realm = currentCharacter.Realm,
                            BattleGroup = currentCharacter.BattleGroup,
                            CharacterClass = currentCharacter.CharacterClass,
                            Race = currentCharacter.Race,
                            Level = currentCharacter.Level,
                            AchievementPoints = currentCharacter.AchievementPoints,
                            Thumbnail = currentCharacter.Thumbnail,
                            CalcClass = currentCharacter.CalcClass,
                            Faction = currentCharacter.Faction,
                            TotalHonorableKills = currentCharacter.TotalHonorableKills
                        },

                        AchievementsData = CharacterExtendedDataManager.MatchCompletedPlayerAchievement(charactersToCompare.Find(x => x.Name.Equals(name)))
                    });
                }
            }
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