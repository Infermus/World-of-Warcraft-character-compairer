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
            return View();
        }

        public IActionResult CompareResult(string firstNickToCompare, string secondNickToCompare)
        {
            string realm = Request.Form["Realm"].ToString();

            RequestLocalization requestLocalization = new RequestLocalization()
            {
                CoreRegionUrlAddress = APIConf.BlizzardAPIWowEUAddress,
                Realm = new Realm(realm, "en_GB")
            };

            BlizzardAPIResponse characterResponse = RequestsRepository.GetCharacterDataAsJsonAsync(firstNickToCompare, requestLocalization, new System.Collections.Generic.List<WowCharComparerLib.Enums.BlizzardAPIFields.CharacterFields>()).Result;

            RealmStatusController.AddRealmsToList(requestLocalization);
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
