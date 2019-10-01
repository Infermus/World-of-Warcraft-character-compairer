using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WowCharComparerWebApp.Data.Connection;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Data.Wrappers;
using WowCharComparerWebApp.Enums;
using WowCharComparerWebApp.Enums.BlizzardAPIFields;
using WowCharComparerWebApp.Models.APIResponse;
using WowCharComparerWebApp.Models.Servers;

namespace WowCharComparerWebApp.Data.ApiRequests
{
    internal class DataResources
    {
        private ComparerDatabaseContext _comparerDatabaseContext;

        public DataResources(ComparerDatabaseContext comparerDatabaseContext)
        {
            _comparerDatabaseContext = comparerDatabaseContext;
        }

        internal async Task<BlizzardAPIResponse> GetCharacterAchievements(RequestLocalization requestLocalization)
        {
            Uri generatedLink = RequestLinkFormater.GenerateAPIRequestLink(BlizzardAPIProfiles.Data, requestLocalization, 
                                                                                                     new List<KeyValuePair<string, string>>(), 
                                                                                                     BlizzardAPIProfiles.Character.ToString(), 
                                                                                                     EnumDictionaryWrapper.dataResourcesFieldsWrapper[DataResourcesFields.CharacterAchievements]);

            return await new APIDataRequestManager(_comparerDatabaseContext).GetDataByHttpRequest<BlizzardAPIResponse>(generatedLink);
        }
    }
}