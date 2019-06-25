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
        internal Models.CharacterProfile.ItemsModels.Others.Items MatchItemsBonusStatistics(Models.CharacterProfile.ExtendedCharacterModel extendedCharacterModel)
        {
            var dbBonusStats = new DbAccessBonusStats(_comparerDatabaseContext).GetAllBonusStats().ToList();

            if (extendedCharacterModel.Items.Head != null)
                extendedCharacterModel.Items.Head.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Head.Stats);

            if (extendedCharacterModel.Items.Neck != null)
                extendedCharacterModel.Items.Neck.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Neck.Stats);

            if (extendedCharacterModel.Items.Shoulder != null)
                extendedCharacterModel.Items.Shoulder.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Shoulder.Stats);

            if (extendedCharacterModel.Items.Back != null)
                extendedCharacterModel.Items.Back.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Back.Stats);

            if (extendedCharacterModel.Items.Chest != null)
                extendedCharacterModel.Items.Chest.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Chest.Stats);

            if (extendedCharacterModel.Items.Shirt != null)
                extendedCharacterModel.Items.Shirt.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Shirt.Stats);

            if (extendedCharacterModel.Items.Wrist != null)
                extendedCharacterModel.Items.Wrist.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Wrist.Stats);

            if (extendedCharacterModel.Items.Hands != null)
                extendedCharacterModel.Items.Hands.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Hands.Stats);

            if (extendedCharacterModel.Items.Waist != null)
                extendedCharacterModel.Items.Waist.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Waist.Stats);

            if (extendedCharacterModel.Items.Legs != null)
                extendedCharacterModel.Items.Legs.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Legs.Stats);

            if (extendedCharacterModel.Items.Feet != null)
                extendedCharacterModel.Items.Feet.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Feet.Stats);

            if (extendedCharacterModel.Items.Finger1 != null)
                extendedCharacterModel.Items.Finger1.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Finger1.Stats);

            if (extendedCharacterModel.Items.Finger2 != null)
                extendedCharacterModel.Items.Finger2.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Finger2.Stats);

            if (extendedCharacterModel.Items.Trinket1 != null)
                extendedCharacterModel.Items.Trinket1.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Trinket1.Stats);

            if (extendedCharacterModel.Items.Trinket2 != null)
                extendedCharacterModel.Items.Trinket2.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Trinket2.Stats);

            if (extendedCharacterModel.Items.MainHand != null)
                extendedCharacterModel.Items.MainHand.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.MainHand.Stats);

            if (extendedCharacterModel.Items.OffHand != null)
                extendedCharacterModel.Items.OffHand.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.OffHand.Stats);

            if (extendedCharacterModel.Items.Tabard != null)
                extendedCharacterModel.Items.Tabard.Stats = PerformItemBonusStatsMatch(dbBonusStats, extendedCharacterModel.Items.Tabard.Stats);

            return extendedCharacterModel.Items;
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