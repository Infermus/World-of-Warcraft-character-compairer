﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WowCharComparerWebApp.Models.Achievement
{
    public class AchievementsData
    {
        [Key]
        [JsonProperty(PropertyName ="Id")]
        public int ID { get; set; }

        public string Title { get; set; }

        public int Points { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }
        
        public int FactionId { get; set; }

    }
}
