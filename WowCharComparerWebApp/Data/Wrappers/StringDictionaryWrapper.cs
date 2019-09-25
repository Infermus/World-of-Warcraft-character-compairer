using System.Collections.Generic;

namespace WowCharComparerWebApp.Data.Wrappers
{
    public static class StringDictionaryWrapper
    {
        public static Dictionary<string, string> viewLocaleWrapper = new Dictionary<string, string>
        {
            { "Europe", "en_GB" },
            { "America", "en_US" },
            { "Korea", "ko_KR" },
            { "Taiwan", "zh_TW" }
        };
    }
}
