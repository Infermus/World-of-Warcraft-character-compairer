using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WowCharComparerLib
{
    public static class APIConf
    {
        public const string APIKey = "v6nnhsgdtax6u4f4nkdj5q88e56dju64";
        public const string CharacterAPIAddress = "https://eu.api.battle.net/wow/character";

        public const int ApiRequestTimeoutInSec = 1;
    }
}
