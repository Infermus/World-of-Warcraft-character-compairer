using WowCharComparerLib.Models.Localization;

namespace WowCharComparerLib.APIConnection.Models
{
    public class RequestLocalization 
    {
        public string CoreRegionUrlAddress { set; get; } 

        public Realm Realm { get; set; }
    }
}