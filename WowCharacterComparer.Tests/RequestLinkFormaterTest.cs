using System;
using System.Collections.Generic;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Enums;
using WowCharComparerWebApp.Enums.RaiderIO;
using WowCharComparerWebApp.Models.Servers;
using Xunit;

namespace WowCharacterComparer.Tests
{
    // Convention
    // method name : UnitOfWork_StateUnderTest_ExpectedBehavior
    // 1. Arrange
    // 2. Act
    // 3. Assert

    public class RequestLinkFormaterTest
    {
        [Fact]
        public void Generate_APIRequestLink_BasicCharacterComparerRequestUrl()
        {
            // Arrange
            string expectedLink = "https://eu.api.blizzard.com/wow/character/burning-legion/selectus?locale=en_GB";

            // Act
            RequestLocalization requestLocalization = new RequestLocalization()
            {
                CoreRegionUrlAddress = WowCharComparerWebApp.Configuration.APIConf.BlizzardAPIWowEUAddress,
                Realm = new Realm() { Slug = "burning-legion", Locale = "en_GB" }
            };

            Uri actualLink = RequestLinkFormater.GenerateAPIRequestLink(BlizzardAPIProfiles.Character, requestLocalization,
                                                                        new List<KeyValuePair<string, string>>(),
                                                                        requestLocalization.Realm.Slug, "Selectus");
            //Assert
            Assert.Equal(expectedLink, actualLink.AbsoluteUri);
        }

        [Fact]
        public void Generate_RaiderIORequestLink_CharacterRaiderIOComparerRequestUrl()
        {
            string expectedLink = "https://raider.io/api/v1/characters/profile?region=eu&realm=burning-legion&name=wykminiacz&fields=mythic_plus_best_runs%2Cmythic_plus_ranks";

            RequestLocalization requestLocalization = new RequestLocalization()
            {
                CoreRegionUrlAddress = WowCharComparerWebApp.Configuration.APIConf.RaiderIOAdress, // refactor this
                Realm = new Realm() { Slug = "burning-legion", Locale = "en_GB", Timezone = "Europe/Paris" }
            };

            Uri actualLink = RequestLinkFormater.GenerateRaiderIOApiRequestLink(requestLocalization, new List<KeyValuePair<string,string>>()
                                                                    {
                                                                        new KeyValuePair<string, string>("fields","mythic_plus_best_runs%2Cmythic_plus_ranks")
                                                                    },
                                                                    new List<KeyValuePair<string, string>>()
                                                                    {
                                                                        new KeyValuePair<string, string>("Region","eu"),
                                                                        new KeyValuePair<string, string>("realm","burning-legion"),
                                                                        new KeyValuePair<string, string>("name","wykminiacz")
                                                                    },
                                                                    "Wykminiacz");

            //Assert
            Assert.Equal(expectedLink, actualLink.AbsoluteUri);

        }
    }
}