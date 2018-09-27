using System.Collections.Generic;
using WowCharComparerLib.APIConnection;
using WowCharComparerLib.APIConnection.Models;
using WowCharComparerLib.Models.Localization;

namespace WowCharComparerWebApp.Models
{
    public class RealmStatusManager
    {
        public static List<string> GetRealmsList(string region)
        {

            RequestLocalization requestLocalization = new RequestLocalization()
            {
                Realm = new Realm() { Locale = region }
            };

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
