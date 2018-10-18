
namespace WowCharComparerWebApp.Models.Achievement
{
    public class AchievementsData
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Points { get; set; }

        public string Description { get; set; }

        public string Reward { get; set; }

        public RewardItems [] RewardItems { get; set; }

        public string Icon { get; set; }

        public Criteria [] Criteria { get; set; }
        
        public bool AccountWide { get; set; }

        public int FactionId { get; set; }
    }
}
