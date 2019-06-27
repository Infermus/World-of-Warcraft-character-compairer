using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WowCharComparerWebApp.Models.Abstract;

namespace WowCharComparerWebApp.Models.Achievement
{
    public class AchievementsData : DatabaseTableModel
    {
        [Key]
        [JsonProperty(PropertyName ="Id")]
        public int ID { get; set; }

        public string Title { get; set; }

        public int Points { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }
        
        public int FactionId { get; set; }

        [ForeignKey("AchievementCategory")]
        public int AchievementCategoryID { get; set; }
    }
}
