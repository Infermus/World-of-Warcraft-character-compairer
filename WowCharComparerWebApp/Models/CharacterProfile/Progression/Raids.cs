
namespace WowCharComparerWebApp.Models.CharacterProfile.Progression
{
    public class Raids
    {
        public string Name { get; set; }

        public int Lfr { get; set; }

        public int Normal { get; set; }

        public int Heroic { get; set; }

        public int Mythic { get; set; }

        public int Id { get; set; }

        public Bosses [] Bosses { get; set; }
    }
}
