using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WowCharComparerWebApp.Configuration;
using WowCharComparerWebApp.Data.ApiRequests;
using WowCharComparerWebApp.Data.Connection;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Data.Database.Repository.Others;
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
        private readonly ComparerDatabaseContext _comparerDatabaseContext;
        private readonly IAPIDataRequestManager _iAPIDataRequestManager;

        private static List<string> currentRealmListPlayerLeft;
        private static List<string> currentRealmListPlayerRight;

        public CharacterComparatorController(ComparerDatabaseContext comparerDatabaseContext, IAPIDataRequestManager iAPIDataRequestManager)
        {
            _comparerDatabaseContext = comparerDatabaseContext;
            _iAPIDataRequestManager = iAPIDataRequestManager;
        }

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

        #region Selecting region actions (Experimental)

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
            return StatusCode(404);
        }

        public IActionResult TestActionTwo()
        {
            new DbAccessBonusStats(_comparerDatabaseContext).InsertBonusStatsTableFromJsonFile();
            return Content("Action two - executed");
        }

        public IActionResult TestActionThree()
        {
            string returnContent = string.Empty;

            var data = new DataResources(_comparerDatabaseContext).GetCharacterAchievements(new RequestLocalization()
            {
                CoreRegionUrlAddress = APIConf.BlizzadAPIAddressWrapper[Region.Europe],
                Realm = new Realm() { Locale = EULocale.en_GB.ToString() }
            });

            if (data.Result.Exception is null)
            {
                Achievement parsedResult = JsonProcessing.DeserializeJsonData<Achievement>(data.Result.Data);
                new DbAccessAchievements(_comparerDatabaseContext).InsertAllAchievementsDataFromApiRequest(parsedResult);

                returnContent = "Test action three executed - achievements insertion to database";
            }
            else
            {
                returnContent = data.Result.Exception.Message;
            }

            return Content(returnContent);
        }

        public IActionResult ComparePlayers(ExtendedCharacterModel firstPlayer, ExtendedCharacterModel secondPlayer)
        {
            List<ProcessedCharacterModel> processedCharacterData = new List<ProcessedCharacterModel>();

            //TODO Get input from view to fill up request localization
            RequestLocalization requestLocalization = new RequestLocalization()
            {
                CoreRegionUrlAddress = APIConf.BlizzadAPIAddressWrapper[Region.Europe], // refactor this
                Realm = new Realm() { Slug = "burning-legion", Locale = "en_GB", Timezone = "Europe/Paris" }
            };

            foreach (string characterName in new List<string>() { firstPlayer.Name, secondPlayer.Name })
            {
                //TODO Get input from view to fill up character fields (check boxes which determines what to compare)
                var result = new CharacterRequests(_iAPIDataRequestManager).GetCharacterDataAsJsonAsync(characterName, requestLocalization,
                                                                            new List<CharacterFields>()
                                                                            {
                                                                                CharacterFields.Items,
                                                                                CharacterFields.Achievements
                                                                            }).Result;

                ExtendedCharacterModel currentCharacter = JsonProcessing.DeserializeJsonData<ExtendedCharacterModel>(result.Data);
                CharacterExtendedDataManager characterDataManager = new CharacterExtendedDataManager(_comparerDatabaseContext);

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

                    AchievementsData = characterDataManager.MatchCompletedPlayerAchievement(currentCharacter),
                    Items = characterDataManager.MatchItemsBonusStatistics(currentCharacter)
                });
            }

            return View("CompareResult");
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

                RealmsRequests realmsRequests = new RealmsRequests(_comparerDatabaseContext);
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