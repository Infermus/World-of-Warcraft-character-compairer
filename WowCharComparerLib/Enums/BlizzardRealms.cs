using System.Collections.Generic;

namespace WowCharComparerLib.Enums
{
    public class BlizzardRealms
    {
        public static string GetWrappedBlizzardRealm(BlizzardRealmEnum realm)
        {
            string wrappedRealm = string.Empty;

            if (blizzardRealmsWrapper.ContainsKey(realm))
            {
                wrappedRealm = blizzardRealmsWrapper[realm];
            }

            return wrappedRealm;
        }

        public enum BlizzardRealmEnum
        {
            Aegwynn,
            AeriePeak,
            Agamaggan,
            BurningLegion
            //TODO MORE REALMS
        }

        private static Dictionary<BlizzardRealmEnum, string> blizzardRealmsWrapper = new Dictionary<BlizzardRealmEnum, string>()
        {
            {BlizzardRealmEnum.Aegwynn, "aegwynn"},
            {BlizzardRealmEnum.BurningLegion, "burning-legion" }
            //TODO wrap all
        };
    }
}