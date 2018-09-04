using Newtonsoft.Json;
using WowCharComparerLib.Models.CharacterProfile;
using WowCharComparerLib.Models.CharacterProfile.Feed;
using WowCharComparerLib.Models.CharacterProfile.Guild;
using WowCharComparerLib.Models.CharacterProfile.HunterPets;
using WowCharComparerLib.Models.CharacterProfile.Items;
using WowCharComparerLib.Models.CharacterProfile.Mounts;
using WowCharComparerLib.Models.CharacterProfile.Pets;
using WowCharComparerLib.Models.CharacterProfile.Progression;
using WowCharComparerLib.Models.CharacterProfile.Pvp;
using WowCharComparerLib.Models.CharacterProfile.Reputation;
using WowCharComparerLib.Models.CharacterProfile.Statistics;
using WowCharComparerLib.Models.CharacterProfile.Talents;

namespace WowCharComparerLib.Models
{
    public class CharacterModel
    {
        public long LastModified { get; set; }

        public string Name { get; set; }

        public string Realm { get; set; }

        public string BattleGroup { get; set; }

        [JsonProperty(PropertyName = "Class")] 
        public int CharacterClass {get; set;}

        public int Race { get; set; }

        public int Level { get; set; }

        public int AchievementPoints { get; set; }

        public string Thumbnail { get; set; }

        public char CalcClass { get; set; }

        public int Faction { get; set; }

        public int TotalHonorableKills { get; set; }

        public Appearance Appearance { get; set; }

        public Feed Feed { get; set; }

        public Achievements Achievements { get; set; }

        public Guild Guild { get; set; }

        public HunterPets [] HunterPets { get; set; }

        public Items Items { get; set; }

        public Mounts Mounts { get; set; }

        public Pets Pets { get; set; }

        public PetSlots[] PetSlots { get; set; }

        public Professions Professions { get; set; }

        public Progression Progression { get; set; }

        public Pvp Pvp { get; set; }

        public int [] Quests { get; set; }

        public Reputation [] Reputation { get; set; }

        public Statistics Statistics { get; set; }

        public CharacterProfile.Stats Stats { get; set; }

        public Talents [] Talents { get; set; }

        public Titles [] Titles { get; set; }
    }
}