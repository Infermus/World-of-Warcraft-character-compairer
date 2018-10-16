using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WowCharComparerWebApp.Models;

namespace WowCharComparerWebApp.Data
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
                    //EUgdMwWv3xVgi0fERGdGHwP6EvSaPdfB58
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "EUgdMwWv3xVgi0fERGdGHwP6EvSaPdfB58");
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