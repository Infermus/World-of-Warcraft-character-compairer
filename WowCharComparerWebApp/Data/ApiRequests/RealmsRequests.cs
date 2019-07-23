using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowCharComparerWebApp.Configuration;
using WowCharComparerWebApp.Data.Connection;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Enums;
using WowCharComparerWebApp.Models.APIResponse;
using WowCharComparerWebApp.Models.Servers;

namespace WowCharComparerWebApp.Data.ApiRequests
{
    internal class RealmsRequests
    {
        private ComparerDatabaseContext _comparerDatabaseContext;

        internal RealmsRequests(ComparerDatabaseContext comparerDatabaseContext)
        {
            _comparerDatabaseContext = comparerDatabaseContext;
        }

        internal async Task<List<string>> GetRealmListByRegion(Region region)
        {
            List<string> realmsNames = new List<string>();
            string regionCoreAdress = string.Empty;

            try
            {
                if (APIConf.BlizzadAPIAddressWrapper.TryGetValue(region, out regionCoreAdress) == false)
                {
                    throw new KeyNotFoundException($"Cannot find region in {APIConf.BlizzadAPIAddressWrapper.GetType().Name} dictionary");
                }

                var requestLocalization = new RequestLocalization()
                {
                    CoreRegionUrlAddress = regionCoreAdress,
                };

                RealmsRequests realmsRequests = new RealmsRequests(_comparerDatabaseContext);

                var parameters = new List<KeyValuePair<string, string>>();
                Uri uriAddress = RequestLinkFormater.GenerateAPIRequestLink(BlizzardAPIProfiles.Realm, requestLocalization, parameters, "status"); //TODO replace string
                var realmResponse = await new APIDataRequestManager(_comparerDatabaseContext).GetDataByHttpRequest<BlizzardAPIResponse>(uriAddress);
                RealmStatus realmStatus = JsonProcessing.DeserializeJsonData<RealmStatus>(realmResponse.Data);

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
