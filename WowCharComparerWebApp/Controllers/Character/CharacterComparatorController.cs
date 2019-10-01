using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WowCharComparerWebApp.Configuration;
using WowCharComparerWebApp.Data.ApiRequests;
using WowCharComparerWebApp.Data.Connection;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Data.Wrappers;
using WowCharComparerWebApp.Enums;
using WowCharComparerWebApp.Enums.BlizzardAPIFields;
using WowCharComparerWebApp.Logic.Character;
using WowCharComparerWebApp.Logic.Character.AchievementPoints;
using WowCharComparerWebApp.Logic.Character.Collectables;
using WowCharComparerWebApp.Logic.Character.Pvp;
using WowCharComparerWebApp.Logic.Character.Statistics;
using WowCharComparerWebApp.Logic.DataResources;
using WowCharComparerWebApp.Logic.HeartOfAzeroth;
using WowCharComparerWebApp.Logic.ItemLevel;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Servers;
using WowCharComparerWebApp.ViewModel.CharacterProfile;

namespace WowCharComparerWebApp.Controllers.CharacterControllers
{
    public class CharacterComparatorController : Controller
    {
        private readonly ComparerDatabaseContext _comparerDatabaseContext;
        private readonly IAPIDataRequestManager _iAPIDataRequestManager;

        private static List<string> currentRealmListPlayerLeft;
        private static List<string> currentRealmListPlayerRight;

        private static CompareResultViewModel compareResultViewModel;

        //private static List<ProcessedCharacterViewModel> processedCharacterData; // TODO: found better way pass data from ajax request to compare result view

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

        [HttpPost]
        public async Task<IActionResult> CompareCharacters(ExtendedCharacterModel firstCharacter, ExtendedCharacterModel secondCharacter)
        {
            var charactersToCompare = new List<ExtendedCharacterModel>() { firstCharacter, secondCharacter };
            var processedCharacterData = new List<ProcessedCharacterViewModel>();

            foreach (ExtendedCharacterModel character in charactersToCompare)
            {
                RequestLocalization requestLocalization = new RequestLocalization()
                {
                    CoreRegionUrlAddress = APIConf.BlizzadAPIAddressWrapper.ContainsKey(EnumDictionaryWrapper.viewRegionsWrapper.ContainsKey(character.Region) ?
                                            EnumDictionaryWrapper.viewRegionsWrapper[character.Region]
                                            : throw new NotSupportedException("Region not supported")) ?
                                                APIConf.BlizzadAPIAddressWrapper[EnumDictionaryWrapper.viewRegionsWrapper[character.Region]]
                                                : throw new NotSupportedException("Core region url not supported"),

                    Realm = new Realm()
                    {
                        Slug = character.ServerName.ToLower().Replace(" ", "-"),
                        Locale = StringDictionaryWrapper.viewLocaleWrapper.ContainsKey(character.Region) ?
                                    StringDictionaryWrapper.viewLocaleWrapper[character.Region]
                                    : throw new NotSupportedException("Locale not supported"),
                    }
                };

                var selectedCharacterFields = new List<CharacterFields>();

                //TODO find better way to process it
                if (character.ItemsField)
                    selectedCharacterFields.Add(CharacterFields.Items);
                if (character.AchievementsField)
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

                processedCharacterData.Add(new ProcessedCharacterViewModel()
                {
                    BasicCharacterData = new BasicCharacterViewModel
                    {
                        Name = currentCharacter.Name,
                        Class = IntDictionaryWrapper.characterClassWrapper.ContainsKey(currentCharacter.CharacterClass) ?
                        IntDictionaryWrapper.characterClassWrapper[currentCharacter.CharacterClass] : string.Empty,
                        Race = IntDictionaryWrapper.characterRaceWrapper.ContainsKey(currentCharacter.Race) ?
                                IntDictionaryWrapper.characterRaceWrapper[currentCharacter.Race] : string.Empty,
                        Level = currentCharacter.Level,
                        AchievementPoints = currentCharacter.AchievementPoints,
                        Thumbnail = currentCharacter.Thumbnail,
                        Faction = currentCharacter.Faction.ToString(),
                        TotalHonorableKills = currentCharacter.TotalHonorableKills
                    },

                    AchievementsData = characterDataManager.MatchCompletedPlayerAchievement(currentCharacter),
                    Items = characterDataManager.MatchItemsBonusStatistics(currentCharacter.Items),
                    Progression = currentCharacter.Progression ?? new Models.CharacterProfile.ProgressionModels.Progression(),
                    Pvp = currentCharacter.Pvp ?? new Models.CharacterProfile.PvpModels.Pvp(),
                    Reputation = currentCharacter.Reputation ?? new List<Reputation>().ToArray(),
                    Statistics = currentCharacter.Statistics ?? new Models.CharacterProfile.StatisticsModels.Statistics(),
                    Talents = currentCharacter.Talents ?? new List<Models.CharacterProfile.TalentsModels.Talents>().ToArray(),
                    CharStats = currentCharacter.Stats ?? new Stats(),
                    Mounts = currentCharacter.Mounts ?? new Models.CharacterProfile.MountsModels.Mounts(),
                    Pets = currentCharacter.Pets ?? new Models.CharacterProfile.PetsModels.Pets()
                });
            }

            //TODO: Maybe those whole compilers object should be moved to 1 class ?
            compareResultViewModel = new CompareResultViewModel()
            {
                ProcessedCharacterViewModel = processedCharacterData,
                AchievementPointsCompareResult = new AchievementPointsComparer().CompareAchievementPoints(processedCharacterData),
                ArenaRatingsCompareResult = new RatingComparer().CompareRating(processedCharacterData),
                HeartOfAzerothCompareResult = new HeartOfAzerothComparer().CompareHeartOfAzerothLevel(processedCharacterData),
                HonorableKillsCompareResult = new HonorableKillsComparer().CompareHonorableKills(processedCharacterData),
                ItemLevelCompareResult = new ItemLevelComparer().CompareCharactersItemLevel(processedCharacterData),
                MiniPetsCompareResult = new MiniPetsComparer().CompareMiniPets(processedCharacterData),
                MountsCompareResult = new MountsComparer().CompareMounts(processedCharacterData),
                StatisticsCompareResult = new StatisticsComparer().CompareCharacterStatistics(processedCharacterData)
            };

            return Json(new
            {
                message = "success",
                url = Url.Action(nameof(RedirectToComparatorResult), GetType().Name.Remove(GetType().Name.Length - "Controller".Length))
            });
        }

        [Route("compare_result")]
        public IActionResult RedirectToComparatorResult()
        {
            return View("CompareCharacters", compareResultViewModel);
        }
    }
}