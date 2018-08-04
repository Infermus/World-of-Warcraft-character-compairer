using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WowCharComparerLib
{
    public class APIConnection
    {
        HttpClient httpClient = new HttpClient();

        public HttpClient ConfigureHttpClient(Uri uriAddress, int timeoutSec)
        {
            httpClient.BaseAddress = uriAddress;
            httpClient.Timeout = TimeSpan.FromMilliseconds(timeoutSec);

            return httpClient;
        }

        public async Task<string> GetResponseFromAPI(HttpClient httpClient)
        {
            Task<string> apiDataTask = httpClient.GetStringAsync(httpClient.BaseAddress);
            return await apiDataTask;
        }
    }
}
