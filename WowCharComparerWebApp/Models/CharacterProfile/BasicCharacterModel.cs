using Newtonsoft.Json;
using WowCharComparerWebApp.Enums.Character;

namespace WowCharComparerWebApp.Models.CharacterProfile
{
    public class BasicCharacterModel : ServerCharacterModel
    {
        public double LastModified { get; set; }

        public string Name { get; set; }

        public string BattleGroup { get; set; }

        [JsonProperty(PropertyName = "Class")]
        public int CharacterClass { get; set; }

        public int Race { get; set; }

        public int Level { get; set; }

        public int AchievementPoints { get; set; }

        public string Thumbnail { get; set; }

        public char CalcClass { get; set; }

        public Faction Faction { get; set; }

        public int TotalHonorableKills { get; set; }
    }
}
