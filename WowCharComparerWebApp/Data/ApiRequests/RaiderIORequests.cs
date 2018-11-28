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

            List<KeyValuePair<string, string>> characterParams = new List<KeyValuePair<string, string>>();

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

                characterParams.Add(new KeyValuePair<string, string>("fields", localFields));
            }

            Uri uriAddress = RequestLinkFormater.GenerateRaiderIOApiRequestLink(requestLocalization, characterParams, characterName);

            return await APIDataRequestManager.GetDataByHttpRequest<RaiderIOAPIResponse>(uriAddress);
        }
    }
}
