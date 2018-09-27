using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using WowCharComparerLib.APIConnection;
using WowCharComparerLib.APIConnection.Models;
using WowCharComparerLib.Configuration;
using WowCharComparerLib.Enums;
using WowCharComparerLib.Models;
using WowCharComparerLib.Models.Localization;

namespace WowCharComparerWebApp.Controllers.CharacterControllers
{
    public class CharacterComparatorController : Controller
    {
        public IActionResult Main()
        {
            string region = "en_GB"; // change it to real input

            List<string> realms = new List<string>();

            RequestLocalization requestLocalization = new RequestLocalization()
            {
                Realm = new Realm() { Locale = region }
            };

            var realmResponse = RequestsRepository.GetRealmsDataAsJsonAsync(requestLocalization);

            RealmStatus realmStatus = WowCharComparerLib.APIConnection.Helpers.ResponseResultFormater.DeserializeJsonData<RealmStatus>(realmResponse.Result.Data);
            foreach (Realm realmsData in realmStatus.Realms)
            {
                realms.Add(realmsData.Name);
            }

            ViewBag.ListOfRealms = realms;



            return View();
        }

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
            string realm = Request.Form["Realm"].ToString();

            RequestLocalization requestLocalization = new RequestLocalization()
            {
                CoreRegionUrlAddress = APIConf.BlizzardAPIWowEUAddress,
                Realm = new Realm()
                {
                    Slug = realm,
                    Locale = "en_GB"
                }
            };

            BlizzardAPIResponse characterResponse = RequestsRepository.GetCharacterDataAsJsonAsync(firstNickToCompare, requestLocalization, new System.Collections.Generic.List<WowCharComparerLib.Enums.BlizzardAPIFields.CharacterFields>()).Result;

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
