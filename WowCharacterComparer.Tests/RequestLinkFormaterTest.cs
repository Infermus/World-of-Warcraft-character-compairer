using System;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Enums;
using System.Collections.Generic;
using Xunit;
using WowCharComparerWebApp.Models.Servers;

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
    }
}