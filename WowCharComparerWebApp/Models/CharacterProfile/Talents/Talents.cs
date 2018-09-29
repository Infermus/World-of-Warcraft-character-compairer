using Newtonsoft.Json;

namespace WowCharComparerWebApp.Models.CharacterProfile.Talents
{
    public class Talents
    {
        public bool Selected { get; set; }

        [JsonProperty(PropertyName = "Talents")]
        public Talent [] Talent { get; set; }

        [JsonProperty(PropertyName = "Spec")]
        public Spec Specs { get; set; }

        public string CalcTalent { get; set; }

        public string CalcSpec { get; set; }
    }
}
