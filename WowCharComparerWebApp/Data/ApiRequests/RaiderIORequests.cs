using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowCharComparerWebApp.Data.Connection;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Enums;
using WowCharComparerWebApp.Enums.RaiderIO;
using WowCharComparerWebApp.Models.APIResponse;
using WowCharComparerWebApp.Models.Servers;

namespace WowCharComparerWebApp.Data.ApiRequests
{
    public class RaiderIORequests
    {
        public static async Task<RaiderIOAPIResponse> GetRaiderIODataAsync(string characterName, RequestLocalization requestLocalization, List<RaiderIOCharacterFields> characterFields)
        {
            string region = requestLocalization.Realm.Timezone == "Europe/Paris" ? "eu" : throw new Exception("Choosed realm is not European");

            List<KeyValuePair<string, string>> fields = new List<KeyValuePair<string, string>>();

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(RaiderIOCharacterParams.Region.ToString(), region),
                new KeyValuePair<string, string>(RaiderIOCharacterParams.Realm.ToString(), requestLocalization.Realm.Slug),
                new KeyValuePair<string, string>(RaiderIOCharacterParams.Name.ToString(), characterName)
            };

            // check if there is any additional parameters to get. If not - just return basic informations
            if (characterFields.Any())
            {
                string localFields = string.Empty;

                foreach (RaiderIOCharacterFields field in characterFields)
                {
                    string wrappedField = EnumDictonaryWrapper.rioCharacterFieldWrapper[field];
                    localFields = localFields.AddFieldToUrl(wrappedField);

                    localFields = localFields.EndsWith("+") ? localFields.Remove(localFields.Length - 1, 1)
                                           : localFields;
                }

                localFields = localFields.EndsWith("%2C") ? localFields.Remove(localFields.Length - 3, 3) // remove join parameters symbol at ending field
                                                           : localFields;

                fields.Add(new KeyValuePair<string, string>("fields", localFields));
            }

            Uri uriAddress = RequestLinkFormater.GenerateRaiderIOApiRequestLink(fields, parameters);

            return await APIDataRequestManager.GetDataByHttpRequest<RaiderIOAPIResponse>(uriAddress);
        }
    }
}
