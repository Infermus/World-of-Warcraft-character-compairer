using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WowCharComparerLib
{
    public class BlizzardAPIResponse
    {
        public string Data { get; set; }
        public Exception Exception { get; set; } = null;
    }

    public class BlizzardAPIManager
    {
        public static async Task<BlizzardAPIResponse> GetCharacterDataAsJsonAsync(string characterName, BlizzardRealms realm)
        {
            BlizzardAPIResponse blizzardAPIResponse = new BlizzardAPIResponse();
            Uri uriAddress = GenerateAPIRequestLink(characterName, realm);

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    blizzardAPIResponse.Data = await httpClient.GetStringAsync(uriAddress.AbsoluteUri);
                }
            }
            catch(Exception ex)
            {
                blizzardAPIResponse = new BlizzardAPIResponse()
                {
                    Data = string.Empty,
                    Exception = ex
                };
            }

            return blizzardAPIResponse;
        }

        public static BlizzardAPIResponse GetCharacterDataAsJson(string characterName, BlizzardRealms realm)
        {
            BlizzardAPIResponse blizzardAPIResponse = new BlizzardAPIResponse();
            Uri uriAddress = GenerateAPIRequestLink(characterName, realm);

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    blizzardAPIResponse.Data = httpClient.GetStringAsync(uriAddress.AbsoluteUri).Result;
                }
            }
            catch(Exception ex)
            {
                blizzardAPIResponse = new BlizzardAPIResponse()
                {
                    Data = string.Empty,
                    Exception = ex
                };
            }

            return blizzardAPIResponse;
        }

        private static Uri GenerateAPIRequestLink(string characterName, BlizzardRealms realm)
        {
            string generatedLink = String.Format("{0}/{1}/{2}?locale=en_GB&{3}", APIConf.BlizzardAPICharacterAddress, realm.ToString(), characterName, APIConf.APIKey);
            return new Uri(generatedLink);
        }
    }
}
