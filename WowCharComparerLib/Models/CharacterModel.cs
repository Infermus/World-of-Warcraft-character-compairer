using Newtonsoft.Json;

namespace WowCharComparerLib.Models
{
    public class CharacterModel
    {
        public long LastModified { get; set; }

        public string Name { get; set; }

        public string Realm { get; set; }

        public string Battlegroup { get; set; }

        [JsonProperty(PropertyName = "class")] 
        public int CharacterClass {get; set;}

        public int Race { get; set; }

        public int Level { get; set; }

        public int AchievementPoints { get; set; }

        public string Thumbnail { get; set; }

        public char CalcClass { get; set; }

        public int Faction { get; set; }

        public int TotalHonorableKills { get; set; }
    }
}