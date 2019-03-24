
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Models.Achievement;
using WowCharComparerWebApp.Models.CharacterProfile;
using System.Linq;
using System;

namespace WowCharComparerWebApp.Logic.DataResources
{
    public class PlayersAchievements
    {
        public static IEnumerable<AchievementsData> CompareAchievements(List<CharacterModel> parsedJson)
        {
            using (ComparerDatabaseContext db = new ComparerDatabaseContext())
            {
                List<AchievementsData> achievementsDatasDB = new List<AchievementsData>();
                achievementsDatasDB = (from id in db.AchievementsData select id).ToList();

                var matchedAchievementOnPlayer = achievementsDatasDB.Join(parsedJson[0].Achievements.AchievementsCompleted.ToList(),
                                                                          dbAchievement => dbAchievement.ID,
                                                                          parsedJsonAchievement => parsedJsonAchievement,
                                                                          (dbAchievement, parsedJsonAchievement) => dbAchievement);            
                return matchedAchievementOnPlayer;
            }
        }
        //(Int32.Parse(achievementToCheck)
    }
}
