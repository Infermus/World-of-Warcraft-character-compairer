using System;
using System.Collections.Generic;
using WowCharComparerLib.APIConnection.Models;
using WowCharComparerLib.Configuration;
using WowCharComparerLib.Enums;

namespace WowCharComparerLib.APIConnection.Helpers
{
    internal static class RequestLinkFormater
    {
        internal static Uri GenerateAPIRequestLink(BlizzardAPIProfiles profile, RequestLocalization requestLocalization, List<KeyValuePair<string, string>> parameters, string endPointPart1 = null, string endPointPart2 = null)
        {
            string processingUriAddress = string.Empty;

            processingUriAddress = processingUriAddress.AddToEndPointSampleToUrl(requestLocalization.CoreRegionUrlAddress);
            processingUriAddress = processingUriAddress.AddToEndPointSampleToUrl(profile.ToString().ToLower());
            processingUriAddress = processingUriAddress.AddToEndPointSampleToUrl(endPointPart1);
            processingUriAddress = processingUriAddress.AddToEndPointSampleToUrl(endPointPart2);

            processingUriAddress = processingUriAddress.EndsWith("/") ? processingUriAddress.Remove(processingUriAddress.Length - 1, 1) : processingUriAddress;

            parameters.Add(new KeyValuePair<string, string>("?locale", requestLocalization.Realm.Locale));
            parameters.Add(new KeyValuePair<string, string>("apikey", APIConf.APIKey));

            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                processingUriAddress = processingUriAddress.AddParameterToUrl(parameter.Key + "=" + parameter.Value);
            }

            processingUriAddress = processingUriAddress.EndsWith("&") ? processingUriAddress.Remove(processingUriAddress.Length - 1, 1) : processingUriAddress;

            return new Uri(processingUriAddress);
        }

        private static string AddToEndPointSampleToUrl(this string baseText, string textToAdd)
        {
            string localText = baseText;

            if (String.IsNullOrEmpty(textToAdd) == false)
            {
                localText = baseText + textToAdd + "/";
            }

            return localText;
        }

        private static string AddParameterToUrl(this string baseText, string textToAdd)
        {
            string localText = baseText;

            if (String.IsNullOrEmpty(textToAdd) == false)
            {
                localText = baseText + textToAdd + "&";
            }

            return localText;
        }

        internal static string AddFieldToUrl(this string baseText, string textToAdd)
        {
            string localText = baseText;

            if (String.IsNullOrEmpty(textToAdd) == false)
            {
                localText = baseText + textToAdd + "%2C+";
            }

            return localText;
        }
    }
}
