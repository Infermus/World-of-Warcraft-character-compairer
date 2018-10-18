using Newtonsoft.Json;
using WowCharComparerWebApp.Models.CharacterProfile.FeedModels;
using WowCharComparerWebApp.Models.CharacterProfile.GuildModels;
using WowCharComparerWebApp.Models.CharacterProfile.HunterPetsModels;
using WowCharComparerWebApp.Models.CharacterProfile.MountsModels;
using WowCharComparerWebApp.Models.CharacterProfile.PetsModels;
using WowCharComparerWebApp.Models.CharacterProfile.ProfessionModels;
using WowCharComparerWebApp.Models.CharacterProfile.ProgressionModels;
using WowCharComparerWebApp.Models.CharacterProfile.PvpModels;
using WowCharComparerWebApp.Models.CharacterProfile.TalentsModels;

namespace WowCharComparerWebApp.Models.CharacterProfile
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

        public ItemsModels.Others.Items Items { get; set; }

        public Mounts Mounts { get; set; }

        public Pets Pets { get; set; }

        public PetSlots[] PetSlots { get; set; }

        public Professions Professions { get; set; }

        public Progression Progression { get; set; }

        public Pvp Pvp { get; set; }

        public int [] Quests { get; set; }

        public Reputation[] Reputation { get; set; }

        public StatisticsModels.Statistics Statistics { get; set; }

        public Stats Stats { get; set; }

        public Talents [] Talents { get; set; }

        public Titles [] Titles { get; set; }
    }
}