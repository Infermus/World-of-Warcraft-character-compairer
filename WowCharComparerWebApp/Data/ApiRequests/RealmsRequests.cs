using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowCharComparerWebApp.Data.Connection;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Enums;
using WowCharComparerWebApp.Models.APIResponse;
using WowCharComparerWebApp.Models.Servers;

namespace WowCharComparerWebApp.Data.ApiRequests
{
    public static class RealmsRequests
    {
        public static async Task<BlizzardAPIResponse> GetRealmsDataAsJsonAsync(RequestLocalization requestLocalization)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            Uri uriAddress = RequestLinkFormater.GenerateAPIRequestLink(BlizzardAPIProfiles.Realm, requestLocalization, parameters, "status"); //TODO replace string
            

            return await APIDataRequestManager.GetDataByHttpRequest<BlizzardAPIResponse>(uriAddress);
        }
    }
}
