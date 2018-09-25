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

namespace WowCharComparerWebApp.Controllers
{
    public class RealmStatusController
    {
        public static List<string> AddRealmsToList(RequestLocalization requestLocalization)
        {
            var realmResponse = RequestsRepository.GetRealmsDataAsJsonAsync(requestLocalization);

            RealmStatus realmStatus = WowCharComparerLib.APIConnection.Helpers.ResponseResultFormater.DeserializeJsonData<RealmStatus>(realmResponse.Result.Data);

            List<string> realms = new List<string>();

            foreach (Realm realmsData in realmStatus.Realms)
            {
                realms.Add(realmsData.Name);
            }
            
            return realms;
        }

    }
}
