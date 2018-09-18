using System;
using System.Net.Http;
using System.Threading.Tasks;
using WowCharComparerLib.Configuration;
using WowCharComparerLib.Enums;
using WowCharComparerLib.Models;
using Newtonsoft.Json;
using WowCharComparerLib.Enums.BlizzardAPIFields;
using System.Collections.Generic;
using System.Linq;

namespace WowCharComparerLib.APIConnection
{
    public static class BlizzardAPIManager
    {
        ///<summary>Gets API data for character profile</summary>
        public static async Task<BlizzardAPIResponse> GetCharacterDataAsJsonAsync(Realm realm, string characterName, List<CharacterFields> characterFields)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();

            if (characterFields.Any())
            {
                string localFields = string.Empty;

                foreach (CharacterFields field in characterFields)
                {
                    string wrappedField = EnumDictonaryWrapper.characterFieldsDicWrapper[field];
                    localFields = localFields.AddFieldToUrl(wrappedField);
                }

                localFields = localFields.EndsWith("%2C+") ? localFields.Remove(localFields.Length - 4, 4) : localFields;

                parameters.Add(new KeyValuePair<string, string>("?fields", localFields));
            }

            parameters.Add(new KeyValuePair<string, string>("locale", realm.Locale));
            parameters.Add(new KeyValuePair<string, string>("apikey", APIConf.APIKey));


            Uri uriAddress = GenerateAPIRequestLink(BlizzardAPIProfiles.Character, parameters, realm.Slug, characterName);
            return await GetDataByHttpClient(uriAddress);
        }

        // https://eu.api.battle.net/wow/realm/status?locale=en_GB&apikey=v6nnhsgdtax6u4f4nkdj5q88e56dju64
        public static async Task<BlizzardAPIResponse> GetRealmDataAsJsonAsync(Locale locale, string status)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("?locale", locale.ToString()));
            parameters.Add(new KeyValuePair<string, string>("apikey", APIConf.APIKey));
            Uri uriAddress = GenerateAPIRequestLink(BlizzardAPIProfiles.Realm, parameters, status);
            return await GetDataByHttpClient(uriAddress);
        }


        private static async Task<BlizzardAPIResponse> GetDataByHttpClient(Uri uriAddress)
        {
            BlizzardAPIResponse blizzardAPIResponse = new BlizzardAPIResponse();

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    blizzardAPIResponse.Data = await httpClient.GetStringAsync(uriAddress.AbsoluteUri);
                }
            }
            catch (Exception ex)
            {
                blizzardAPIResponse = new BlizzardAPIResponse()
                {
                    Data = string.Empty,
                    Exception = ex
                };
            }

            return blizzardAPIResponse;
        }

        //example: https://eu.api.battle.net/wow/character/burning-legion/Selectus?locale=en_GB&apikey=v6nnhsgdtax6u4f4nkdj5q88e56dju64
        private static Uri GenerateAPIRequestLink(BlizzardAPIProfiles profile, List<KeyValuePair<string, string>> parameters, string endPointPart1 = null, string endPointPart2 = null)
        {
            string apiHttpAddress = string.Empty;

            apiHttpAddress = apiHttpAddress.AddToEndPointSampleToUrl(APIConf.BlizzardAPICharacterCoreAddress);

            apiHttpAddress = apiHttpAddress.AddToEndPointSampleToUrl(profile.ToString().ToLower());
            apiHttpAddress = apiHttpAddress.AddToEndPointSampleToUrl(endPointPart1);
            apiHttpAddress = apiHttpAddress.AddToEndPointSampleToUrl(endPointPart2);

            apiHttpAddress = apiHttpAddress.EndsWith("/") ? apiHttpAddress.Remove(apiHttpAddress.Length - 1, 1) : apiHttpAddress;

            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                apiHttpAddress = apiHttpAddress.AddParameterToUrl(parameter.Key + "=" + parameter.Value);
            }

            apiHttpAddress = apiHttpAddress.EndsWith("&") ? apiHttpAddress.Remove(apiHttpAddress.Length - 1, 1) : apiHttpAddress;

            return new Uri(apiHttpAddress);
        }

        private static string AddToEndPointSampleToUrl(this string baseText, string textToAdd)
        {
            string localText = baseText;

            if (String.IsNullOrEmpty(textToAdd) == false)
            {
                localText = baseText + textToAdd + "/";
            }

            return localText;
        }

        private static string AddParameterToUrl(this string baseText, string textToAdd)
        {
            string localText = baseText;

            if (String.IsNullOrEmpty(textToAdd) == false)
            {
                localText = baseText + textToAdd + "&";
            }

            return localText;
        }

        private static string AddFieldToUrl(this string baseText, string textToAdd)
        {
            string localText = baseText;

            if (String.IsNullOrEmpty(textToAdd) == false)
            {
                localText = baseText + textToAdd + "%2C+";
            }

            return localText;
        }

        public static T DeserializeJsonData<T>(string jsonToParse) where T : class
        {
            return JsonConvert.DeserializeObject<T>(jsonToParse);
        }
    }
}