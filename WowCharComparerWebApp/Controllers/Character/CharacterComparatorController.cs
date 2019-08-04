using Microsoft.AspNetCore.Mvc;
using System;
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

        public async Task<IActionResult> CompareCharacters(ExtendedCharacterModel firstCharacter, ExtendedCharacterModel secondCharacter)
        {
            var coreRegionUrlAdress = string.Empty;
            var processedCharacterData = new List<ProcessedCharacterModel>();

            var charactersToCompare = new List<ExtendedCharacterModel>() { firstCharacter, secondCharacter };

            foreach (ExtendedCharacterModel character in charactersToCompare)
            {
                RequestLocalization requestLocalization = new RequestLocalization()
                {
                    CoreRegionUrlAddress = APIConf.BlizzadAPIAddressWrapper.ContainsKey(EnumDictonaryWrapper.viewRegionsWrapper.ContainsKey(character.Region) ?
                                            EnumDictonaryWrapper.viewRegionsWrapper[character.Region] 
                                            : throw new NotSupportedException("Region not supported")) ?
                                                APIConf.BlizzadAPIAddressWrapper[EnumDictonaryWrapper.viewRegionsWrapper[character.Region]] 
                                                : throw new NotSupportedException("Core region url nor supported"),

                    Realm = new Realm()
                    {
                        Slug = character.ServerName.ToLower().Replace(" ", "-"),
                        Locale = EnumDictonaryWrapper.viewLocaleWrapper.ContainsKey(character.Region) ?
                                    EnumDictonaryWrapper.viewLocaleWrapper[character.Region]
                                    : throw new NotSupportedException("Locale not supported"),
                    }
                };

                var selectedCharacterFields = new List<CharacterFields>();

                //TODO find better way to process it
                if (character.ItemsField)
                    selectedCharacterFields.Add(CharacterFields.Items);
                if(character.AchievementsField)
                    selectedCharacterFields.Add(CharacterFields.Achievements);
                if (character.ProgressionField)
                    selectedCharacterFields.Add(CharacterFields.Progression);
                if (character.PVPField)
                    selectedCharacterFields.Add(CharacterFields.PVP);
                if (character.ReputationField)
                    selectedCharacterFields.Add(CharacterFields.Reputation);
                if (character.StatisticsField)
                    selectedCharacterFields.Add(CharacterFields.Statistics);
                if (character.TalentsField)
                    selectedCharacterFields.Add(CharacterFields.Talents);

                var result = await new CharacterRequests(_iAPIDataRequestManager).GetCharacterDataAsJsonAsync(character.Name, requestLocalization, selectedCharacterFields);

                if (result.Exception != null)
                    return View("CompareResult", result.Exception.Message);

                ExtendedCharacterModel currentCharacter = JsonProcessing.DeserializeJsonData<ExtendedCharacterModel>(result.Data);
                CharacterExtendedDataManager characterDataManager = new CharacterExtendedDataManager(_comparerDatabaseContext);

                processedCharacterData.Add(new ProcessedCharacterModel()
                {
                    RawCharacterData = new BasicCharacterModel
                    {
                        LastModified = currentCharacter.LastModified,
                        Name = currentCharacter.Name,
                        ServerName = currentCharacter.ServerName,
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

                    AchievementsData = currentCharacter.Achievements is null ? new List<AchievementsData>() : characterDataManager.MatchCompletedPlayerAchievement(currentCharacter),
                    Items = currentCharacter.Items ?? characterDataManager.MatchItemsBonusStatistics(currentCharacter),
                    Progression = currentCharacter.Progression,
                    Pvp = currentCharacter.Pvp,
                    Reputation = currentCharacter.Reputation,
                    Statistics = currentCharacter.Statistics,
                    Talents = currentCharacter.Talents
                });
            }

            return View("CompareResult");
        }
    }
}