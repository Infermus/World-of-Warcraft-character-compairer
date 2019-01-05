using Newtonsoft.Json;

namespace WowCharComparerWebApp.Models.RaiderIO.Character.MythicPlusRanks
{
    public class MythicPlusRanks
    {
        public Overral Ovveral { get; set; }

        public Dps Dps { get; set; }

        public Healer Healer { get; set; }

        public Tank Tank { get; set; }

        [JsonProperty(PropertyName = "class")]
        public PlayerClass PlayerClass { get; set; }

        [JsonProperty(PropertyName = "class_dps")]
        public ClassDps ClassDps { get; set; }

        [JsonProperty(PropertyName = "class_healer")]
        public ClassHealer ClassHealer { get; set; }

        [JsonProperty(PropertyName = "class_tank")]
        public ClassTank ClassTank { get; set; }
    }
}
