using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowCharComparerLib.APIConnection.Helpers;
using WowCharComparerLib.APIConnection.Models;
using WowCharComparerLib.Enums;
using WowCharComparerLib.Enums.BlizzardAPIFields;
using WowCharComparerLib.Models;

namespace WowCharComparerLib.APIConnection
{
    public class RequestsRepository
    {
        //ex: https://eu.api.battle.net/wow/character/burning-legion/Selectus?locale=en_GB&apikey=v6nnhsgdtax6u4f4nkdj5q88e56dju64
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

            return await BlizzardAPIManager.GetDataByHttpClient(uriAddress);
        }


        // https://eu.api.battle.net/wow/realm/status?locale=en_GB&apikey=v6nnhsgdtax6u4f4nkdj5q88e56dju64
        public static async Task<BlizzardAPIResponse> GetRealmsDataAsJsonAsync(RequestLocalization requestLocalization)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            Uri uriAddress = RequestLinkFormater.GenerateAPIRequestLink(BlizzardAPIProfiles.Realm, requestLocalization, parameters, "status");

            return await BlizzardAPIManager.GetDataByHttpClient(uriAddress);
        }
    }
}
