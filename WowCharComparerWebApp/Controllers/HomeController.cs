using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WowCharComparerLib.APIConnection;
using WowCharComparerLib.Enums;
using WowCharComparerLib.Enums.BlizzardAPIFields;
using WowCharComparerLib.Models;
using WowCharComparerWebApp.Models;

namespace WowCharComparerWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //var result = BlizzardAPIManager.GetCharacterDataAsJsonAsync(
            //                                                            new Realm("burning-legion", "en_GB"),
            //                                                            "Avvril",
            //                                                            new List<CharacterFields>()
            //                                                            {
            //                                                                CharacterFields.PVP
            //                                                            }).Result;
            BlizzardAPIResponse response = BlizzardAPIManager.GetRealmDataAsJsonAsync(Locale.en_GB, "status").Result;
            RealmStatus realmStatus = BlizzardAPIManager.DeserializeJsonData<RealmStatus>(response.Data);

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}