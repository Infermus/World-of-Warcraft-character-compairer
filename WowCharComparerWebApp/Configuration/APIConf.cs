using System.Collections.Generic;
using WowCharComparerWebApp.Enums;

namespace WowCharComparerWebApp.Configuration
{
    public static class APIConf //TODO part of this file should go to json configuration file
    {
        public static string WoWCharacterComparerEmailPassword;
        public static string WowCharacterComparerEmail;

        #region WOW API URLS

        public static readonly Dictionary<Region, string> BlizzadAPIAddressWrapper = new Dictionary<Region, string>()
        {
            [Region.Europe] = BlizzardAPIWowEUAddress,
            [Region.America] = BlizzardAPIWowUSAddress,
            [Region.Taiwan] = BlizzardAPIWowTWAddress,
            [Region.Korea] = BlizzardAPIWowKRAddress
        };

        private const string BlizzardAPIWowEUAddress = "https://eu.api.blizzard.com/wow";
        private const string BlizzardAPIWowUSAddress = "https://us.api.blizzard.com/wow";
        private const string BlizzardAPIWowTWAddress = "https://tw.api.blizzard.com/wow";
        private const string BlizzardAPIWowKRAddress = "https://kr.api.blizzard.com/wow";

        public const string BlizzardAPIWowOAuthTokenAdress = "https://us.battle.net/oauth/token";

        #endregion

        #region Raider IO URLS

        public const string RaiderIOAdress = "https://raider.io/api/v1/characters/profile";

        #endregion

        #region API authorizationContentFields

        public const string BlizzardAPIWowClientIDParameter = "client_id";
        public const string BlizzardAPIWowClientSecretParameter = "client_secret";
        public const string BlizzardAPIWowGrantTypeParameter = "grant_type";
        public const string BlizzardAPIWowClientCredentialsParameter = "client_credentials";

        #endregion

        public const string BlizzardAPIWowMainClientID = "9f804904a6014e319255ff6689eec3dd";
    }
}