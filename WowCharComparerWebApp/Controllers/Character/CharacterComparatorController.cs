﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WowCharComparerWebApp.Data.ApiRequests;
using WowCharComparerWebApp.Data.Database.Repository;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Enums.BlizzardAPIFields;
using WowCharComparerWebApp.Enums.Locale;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Servers;

namespace WowCharComparerWebApp.Controllers.CharacterControllers
{
    public class CharacterComparatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestActionOne()
        {

            RequestLocalization requestLocalization = new RequestLocalization()
            {
                CoreRegionUrlAddress = Configuration.APIConf.BlizzardAPIWowEUAddress,
                Realm = new Realm() { Slug = "burning-legion", Locale = "en_GB" }
            };

            var achievementsResourcesData = DataResources.GetCharacterAchievements(requestLocalization);

            var result = CharacterRequests.GetCharacterDataAsJsonAsync("Selectus",
                                                            requestLocalization,
                                                            new List<CharacterFields>()
                                                            {
                                                                 CharacterFields.Items
                                                            }).Result;

            CharacterModel parsedResult = JsonProcessing.DeserializeJsonData<CharacterModel>(result.Data);

            return StatusCode(404);
        }

        public IActionResult TestActionTwo()
        {
            temp_DataPreparation.InsertBonusStatsTableFromJsonFile();

            return Content("Action two - executed");
        }

        public IActionResult TestActionThree()
        {

            var data = DataResources.GetCharacterAchievements(new RequestLocalization()
            {
                CoreRegionUrlAddress = Configuration.APIConf.BlizzardAPIWowEUAddress,
                Realm = new Realm() { Locale = EULocale.en_GB.ToString() }
            });

            return Content(data.Result.Data);
        }

        //[Route("region-selected")]
        public IActionResult LoadRealmData()
        {
            //string region = string.Empty; // TODO CHANGE!!
            //List<string> realms = new List<string>();

            //RequestLocalization requestLocalization = new RequestLocalization()
            //{
            //    Realm = new Realm() { Locale = region }
            //};

            //var realmResponse = RequestsRepository.GetRealmsDataAsJsonAsync(requestLocalization);

            //RealmStatus realmStatus = WowCharComparerLib.APIConnection.Helpers.ResponseResultFormater.DeserializeJsonData<RealmStatus>(realmResponse.Result.Data);
            //foreach (Realm realmsData in realmStatus.Realms)
            //{
            //    realms.Add(realmsData.Name);
            //}

            //ViewBag.ListOfRealms = realms;

            return View();
        }

        public IActionResult CompareResult(string firstNickToCompare, string secondNickToCompare)
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
    }
}

//var result = BlizzardAPIManager.GetCharacterDataAsJsonAsync(
//                                                            new Realm("burning-legion", "en_GB"),
//                                                            "Avvril",
//                                                            new List<CharacterFields>()
//                                                            {
//                                                                CharacterFields.PVP
//                                                            }).Result;
