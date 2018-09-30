
namespace WowCharComparerWebApp.Models.CharacterProfile.GuildModels
{
    public class Guild
    {
        public string Name { get; set; }

        public string Realm { get; set; }

        public string BattleGroup { get; set; }

        public int Members { get; set; }

        public int AchievementPoints { get; set; }

        public Emblem Emblem { get; set; }
    }
}
