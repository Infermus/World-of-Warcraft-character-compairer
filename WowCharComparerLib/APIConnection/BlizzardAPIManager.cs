using System;
using System.Net.Http;
using System.Threading.Tasks;
using WowCharComparerLib.Models;

namespace WowCharComparerLib.APIConnection
{
    internal static class BlizzardAPIManager
    {
        public static async Task<BlizzardAPIResponse> GetDataByHttpClient(Uri uriAddress)
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
    }
}