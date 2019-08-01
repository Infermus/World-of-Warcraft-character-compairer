using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WowCharComparerWebApp.Configuration;
using WowCharComparerWebApp.Data.ApiRequests;
using WowCharComparerWebApp.Data.Connection;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Enums;
using WowCharComparerWebApp.Enums.BlizzardAPIFields;
using WowCharComparerWebApp.Logic.DataResources;
using WowCharComparerWebApp.Models;
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

        public async Task<IActionResult> Index()
        {
            //Note: Local variable to to avoid 2x requests for both players at default;
            List<string> defaultRegionForBothPlayer = await GetRealmsList(Region.Europe);

            currentRealmListPlayerLeft = defaultRegionForBothPlayer;
            currentRealmListPlayerRight = defaultRegionForBothPlayer;

            ViewData["realmsListLeftPlayer"] = currentRealmListPlayerLeft;
            ViewData["realmsListRightPlayer"] = currentRealmListPlayerRight;

            return View();
        }

        /// <summary>
        /// Gets realms list for selected region
        /// </summary>
        /// <param name="region">Region</param>
        /// <returns>List of realms from Blizzard's api request</returns>
        public async Task<List<string>> GetRealmsList(Region region)
        {
            return await new RealmsRequests(_comparerDatabaseContext).GetRealmListByRegion(region);
        }

        public async Task<IActionResult> ComparePlayers(ExtendedCharacterModel firstPlayer, ExtendedCharacterModel secondPlayer)
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
                var result = await new CharacterRequests(_iAPIDataRequestManager).GetCharacterDataAsJsonAsync(characterName, requestLocalization,
                                                                                    new List<CharacterFields>()
                                                                                    {
                                                                                        CharacterFields.Items,
                                                                                        CharacterFields.Achievements
                                                                                    });

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
    }
}