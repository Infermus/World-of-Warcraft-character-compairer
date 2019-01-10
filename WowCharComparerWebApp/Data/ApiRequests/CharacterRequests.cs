using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowCharComparerWebApp.Enums.BlizzardAPIFields;
using WowCharComparerWebApp.Models.APIResponse;
using WowCharComparerWebApp.Models.Servers;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Enums;
using WowCharComparerWebApp.Data.Connection;

namespace WowCharComparerWebApp.Data.ApiRequests
{
    public class CharacterRequests
    {
        public static async Task<BlizzardAPIResponse> GetCharacterDataAsJsonAsync(string characterName, RequestLocalization requestLocalization, List<CharacterFields> characterFields)
        {
            List<KeyValuePair<string, string>> characterParams = new List<KeyValuePair<string, string>>();

            // check if there is any additional parameters to get. If not - just return basic informations
            if (characterFields.Any())
            {
                string localFields = string.Empty;

                foreach (CharacterFields field in characterFields)
                {
                    string wrappedField = EnumDictonaryWrapper.characterFieldsWrapper[field];
                    localFields = localFields.AddFieldToUrl(wrappedField);
                }

                localFields = localFields.EndsWith("%2C+") ? localFields.Remove(localFields.Length - 4, 4) // remove join parameters symbol at ending field
                                                           : localFields;

                characterParams.Add(new KeyValuePair<string, string>("?fields", localFields));
            }
            Uri uriAddress = RequestLinkFormater.GenerateAPIRequestLink(BlizzardAPIProfiles.Character, requestLocalization, characterParams, requestLocalization.Realm.Slug, characterName); // generates link for request

            return await APIDataRequestManager.GetDataByHttpRequest<BlizzardAPIResponse>(uriAddress);
        }
    }
}
