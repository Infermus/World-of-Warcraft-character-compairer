using Newtonsoft.Json;
using System;

namespace WowCharComparerLib.APIConnection.Helpers
{
    public class ResponseResultFormater
    {
        public static T DeserializeJsonData<T>(string jsonToParse) where T : class
        {
            object deserializeObject = null;

            try
            {
                deserializeObject = JsonConvert.DeserializeObject<T>(jsonToParse);
            }
            catch (Exception)
            {
                deserializeObject = null;
            }

            return (T) deserializeObject;
        }
    }
}
