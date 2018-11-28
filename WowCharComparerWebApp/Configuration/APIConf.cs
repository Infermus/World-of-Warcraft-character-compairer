namespace WowCharComparerWebApp.Configuration
{
    public static class APIConf //TODO part of this file should go to json configuration file
    {
        #region WOW API URLS

        public const string BlizzardAPIWowEUAddress = "https://eu.api.blizzard.com/wow";
        public const string BlizzardAPIWowUSAddress = "https://us.api.blizzard.com/wow";
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