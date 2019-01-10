using Newtonsoft.Json;

namespace WowCharComparerWebApp.Models.RaiderIO
{
    public class RaiderIOModel
    {
        public Character.Character Character { get; set; }

        public Guild Guild { get; set; }

        [JsonProperty(PropertyName = "Mythic_Plus")]
        public MythicPlus MythicPlus { get; set; }

        public Raiding Raiding { get; set; }
    }
}
