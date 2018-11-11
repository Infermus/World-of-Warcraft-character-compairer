using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WowCharComparerWebApp.Configuration;
using WowCharComparerWebApp.Data.Database.Repository;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Models;
using WowCharComparerWebApp.Models.Internal;

namespace WowCharComparerWebApp.Data.Connection
{
    internal static class BlizzardAPIManager
    {
        public static async Task<BlizzardAPIResponse> GetDataByHttpRequest(Uri uriAddress)
        {
            BlizzardAPIResponse blizzardAPIResponse = new BlizzardAPIResponse();

            APIClient apiClient = APIAuthorizationDB.GetClientInformation(APIConf.BlizzardAPIWowMainClientID);
            OAuth2Token token = GetBearerAuthenticationToken(apiClient);

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.TokenValue);
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

        private static OAuth2Token GetBearerAuthenticationToken(APIClient apiClient)
        {
            OAuth2Token receivedOAuth2Token = new OAuth2Token();

            FormUrlEncodedContent requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>(APIConf.BlizzardAPIWowClientIDParameter, apiClient.ClientID),
                new KeyValuePair<string, string>(APIConf.BlizzardAPIWowClientSecretParameter, apiClient.ClientSecret),
                new KeyValuePair<string, string>(APIConf.BlizzardAPIWowGrantTypeParameter, APIConf.BlizzardAPIWowClientCredentialsParameter)
            });

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = httpClient.PostAsync(APIConf.BlizzardAPIWowOAuthTokenAdress, requestContent);
                    response.Wait();
                    var responseResult = response.Result.Content.ReadAsStringAsync().Result;
                    receivedOAuth2Token = JsonProcessing.DeserializeJsonData<OAuth2Token>(responseResult);
                }
            }
            catch (Exception)
            {
                //todo hit with nlog
                receivedOAuth2Token = new OAuth2Token();
            }

            return receivedOAuth2Token;
        }
    }
}