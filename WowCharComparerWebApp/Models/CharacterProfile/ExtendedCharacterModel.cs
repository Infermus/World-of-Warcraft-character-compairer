
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.CharacterProfile.FeedModels;
using WowCharComparerWebApp.Models.CharacterProfile.GuildModels;
using WowCharComparerWebApp.Models.CharacterProfile.HunterPetsModels;
using WowCharComparerWebApp.Models.CharacterProfile.MountsModels;
using WowCharComparerWebApp.Models.CharacterProfile.PetsModels;
using WowCharComparerWebApp.Models.CharacterProfile.ProfessionModels;
using WowCharComparerWebApp.Models.CharacterProfile.ProgressionModels;
using WowCharComparerWebApp.Models.CharacterProfile.PvpModels;
using WowCharComparerWebApp.Models.CharacterProfile.TalentsModels;

namespace WowCharComparerWebApp.Models
{
    public class ExtendedCharacterModel : BasicCharacterModel
    {
        public Appearance Appearance { get; set; }

        public Feed Feed { get; set; }

        public Achievements Achievements { get; set; }

        public Guild Guild { get; set; }

        public HunterPets[] HunterPets { get; set; }

        public CharacterProfile.ItemsModels.Others.Items Items { get; set; }

        public Mounts Mounts { get; set; }

        public Pets Pets { get; set; }

        public PetSlots[] PetSlots { get; set; }

        public Professions Professions { get; set; }

        public Progression Progression { get; set; }

        public Pvp Pvp { get; set; }

        public int[] Quests { get; set; }

        public Reputation[] Reputation { get; set; }

        public CharacterProfile.StatisticsModels.Statistics Statistics { get; set; }

        public CharacterProfile.Stats Stats { get; set; }

        public Talents[] Talents { get; set; }

        public Titles[] Titles { get; set; }
    }
}