using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowCharComparerLib.APIConnection.Helpers
{
    internal class ResponseResultFormater
    {
        public static T DeserializeJsonData<T>(string jsonToParse) where T : class
        {
            return JsonConvert.DeserializeObject<T>(jsonToParse);
        }
    }
}
