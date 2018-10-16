using Newtonsoft.Json;
using System;

namespace WowCharComparerWebApp.Data.Helpers
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
            catch (Exception ex)
            {
                deserializeObject = ex.Message;
            }

            return (T)deserializeObject;
        }
    }
}
