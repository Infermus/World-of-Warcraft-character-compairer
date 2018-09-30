using Newtonsoft.Json;

namespace WowCharComparerWebApp.Models.CharacterProfile.PetsModels
{
    public class Pets 
    {
        [JsonProperty(PropertyName = "NumCollected")]
        public int NumberOfCollectedPets { get; set; }

        [JsonProperty(PropertyName = "NumNotCollected")]
        public int NumberOfNotCollectedPets { get; set; }

        public Collected[] Collected { get; set; }

    }
}
