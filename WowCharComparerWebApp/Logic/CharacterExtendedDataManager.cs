using System.Collections.Generic;
using System.Linq;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Data.Database.Repository.Others;
using WowCharComparerWebApp.Models;
using WowCharComparerWebApp.Models.Achievement;

namespace WowCharComparerWebApp.Logic.DataResources
{
    internal class CharacterExtendedDataManager
    {
        private ComparerDatabaseContext _comparerDatabaseContext;

        public CharacterExtendedDataManager(ComparerDatabaseContext comparerDatabaseContext)
        {
            _comparerDatabaseContext = comparerDatabaseContext;
        }

        /// <summary>
        /// Gets completed achievement by player and match them by ID with database ones to create full information about earned achievement
        /// </summary>
        /// <param name="extendedCharacterModel"></param>
        /// <returns></returns>
        internal IEnumerable<AchievementsData> MatchCompletedPlayerAchievement(Models.CharacterProfile.ExtendedCharacterModel extendedCharacterModel)
        {
            if (extendedCharacterModel.Achievements is null)
                return new List<AchievementsData>();

            List<AchievementsData> achievementsDatasDB = new DbAccessAchievements(_comparerDatabaseContext).GetAllAchievementsData().ToList();

             return achievementsDatasDB.Join(extendedCharacterModel.Achievements.AchievementsCompleted.ToList(),
                                            dbAchievement => dbAchievement.ID,
                                            playerAchievement => playerAchievement,
                                            (dbAchievement, playerAchievement) => dbAchievement);
        }

        /// <summary>
        /// Gets items id bonus stats parameters and try to match them to pre-prepared list of bonus stats name
        /// </summary>
        /// <param name="parsedJson"></param>
        internal Models.CharacterProfile.ItemsModels.Others.Items MatchItemsBonusStatistics(Models.CharacterProfile.ItemsModels.Others.Items characterItems)
        {
            if (characterItems is null)
                return new Models.CharacterProfile.ItemsModels.Others.Items();

            var dbBonusStats = new DbAccessBonusStats(_comparerDatabaseContext).GetAllBonusStats().ToList();

            if (characterItems.Head != null)
                characterItems.Head.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Head.Stats);

            if (characterItems.Neck != null)
                characterItems.Neck.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Neck.Stats);

            if (characterItems.Shoulder != null)
                characterItems.Shoulder.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Shoulder.Stats);

            if (characterItems.Back != null)
                characterItems.Back.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Back.Stats);

            if (characterItems.Chest != null)
                characterItems.Chest.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Chest.Stats);

            if (characterItems.Shirt != null)
                characterItems.Shirt.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Shirt.Stats);

            if (characterItems.Wrist != null)
                characterItems.Wrist.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Wrist.Stats);

            if (characterItems.Hands != null)
                characterItems.Hands.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Hands.Stats);

            if (characterItems.Waist != null)
                characterItems.Waist.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Waist.Stats);

            if (characterItems.Legs != null)
                characterItems.Legs.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Legs.Stats);

            if (characterItems.Feet != null)
                characterItems.Feet.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Feet.Stats);

            if (characterItems.Finger1 != null)
                characterItems.Finger1.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Finger1.Stats);

            if (characterItems.Finger2 != null)
                characterItems.Finger2.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Finger2.Stats);

            if (characterItems.Trinket1 != null)
                characterItems.Trinket1.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Trinket1.Stats);

            if (characterItems.Trinket2 != null)
                characterItems.Trinket2.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Trinket2.Stats);

            if (characterItems.MainHand != null)
                characterItems.MainHand.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.MainHand.Stats);

            if (characterItems.OffHand != null)
                characterItems.OffHand.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.OffHand.Stats);

            if (characterItems.Tabard != null)
                characterItems.Tabard.Stats = PerformItemBonusStatsMatch(dbBonusStats, characterItems.Tabard.Stats);

            return characterItems;
        }

        private Models.CharacterProfile.ItemsModels.Others.Stats[] PerformItemBonusStatsMatch(List<BonusStats> dbBonusStats,
                                                                                              Models.CharacterProfile.ItemsModels.Others.Stats[] itemStatistics)
        {
            return dbBonusStats.Join(itemStatistics,
                                     dbStat => dbStat.BonusStatsID,
                                     apiStat => apiStat.Stat,
                                     (dbStat, apiStat) => new Models.CharacterProfile.ItemsModels.Others.Stats()
                                     {
                                         Amount = apiStat.Amount,
                                         BonusStatsID = dbStat.BonusStatsID,
                                         Stat = apiStat.Stat,
                                         Name = dbStat.Name,
                                         ID = dbStat.ID
                                     }).ToArray();
        }
    }
}