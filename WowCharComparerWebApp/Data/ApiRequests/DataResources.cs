using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WowCharComparerWebApp.Data.Connection;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Enums;
using WowCharComparerWebApp.Enums.BlizzardAPIFields;
using WowCharComparerWebApp.Models;
using WowCharComparerWebApp.Models.Servers;

namespace WowCharComparerWebApp.Data.ApiRequests
{
    public static class DataResources
    {
        public static async Task<BlizzardAPIResponse> GetCharacterAchievements(RequestLocalization requestLocalization)
        {
            Uri generatedLink = RequestLinkFormater.GenerateAPIRequestLink(BlizzardAPIProfiles.Data, requestLocalization, 
                                                                                                     new List<KeyValuePair<string, string>>(), 
                                                                                                     BlizzardAPIProfiles.Character.ToString(), 
                                                                                                     EnumDictonaryWrapper.dataResourcesFieldsWrapper[DataResourcesFields.CharacterAchievements]);

            return await BlizzardAPIManager.GetDataByHttpRequest(generatedLink);
        }
    }
}