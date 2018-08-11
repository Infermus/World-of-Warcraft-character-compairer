using System;
using System.Net.Http;
using System.Threading.Tasks;
using WowCharComparerLib.Configuration;
using WowCharComparerLib.Enums;
using WowCharComparerLib.Models;

namespace WowCharComparerLib.APIConnection
{
    public class BlizzardAPIManager
    {
        public static async Task<BlizzardAPIResponse> GetCharacterDataAsJsonAsync(string characterName, string realm, BlizzardLocales locale)
        {
            BlizzardAPIResponse blizzardAPIResponse = new BlizzardAPIResponse();
            Uri uriAddress = GenerateAPIRequestLink(characterName, realm, locale);

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

        public static BlizzardAPIResponse GetCharacterDataAsJson(string characterName, string realm, BlizzardLocales locale)
        {
            BlizzardAPIResponse blizzardAPIResponse = new BlizzardAPIResponse();
            Uri uriAddress = GenerateAPIRequestLink(characterName, realm, locale);

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    blizzardAPIResponse.Data = httpClient.GetStringAsync(uriAddress.AbsoluteUri).Result;
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
        private static Uri GenerateAPIRequestLink(string characterName, BlizzardRealm realm)
        {
            string generatedLink = String.Format("{0}/{1}/{2}?locale={3}&apikey={4}", APIConf.BlizzardAPICharacterAddress,
                                                                                      realm.slag,
                                                                                      characterName,
                                                                                      realm.locale,
                                                                                      APIConf.APIKey);
            return new Uri(generatedLink);
        }
    }
}
