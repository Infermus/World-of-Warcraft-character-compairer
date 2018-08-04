using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WowCharComparerLib;

namespace WowCharConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            APIConnection apiConnection = new APIConnection();

            HttpClient client = apiConnection.ConfigureHttpClient(new Uri(APIConf.CharacterAPIAddress), APIConf.ApiRequestTimeoutInSec);
            HttpResponseMessage response = await apiConnection.GetResponseFromAPI(client);

        }


    }
}
