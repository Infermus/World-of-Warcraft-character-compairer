using System.Collections.Generic;
using System.Linq;
using WowCharComparerWebApp.Builders;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Models;
using WowCharComparerWebApp.Models.Achievement;
using WowCharComparerWebApp.Models.CharacterProfile.ItemsModels.Gear;
using WowCharComparerWebApp.Models.CharacterProfile.ItemsModels.Others;
using WowCharComparerWebApp.Models.Interfaces;
namespace WowCharComparerWebApp.Logic.DataResources
{
    public class CharacterExtendedDataManager
    {
        /// <summary>
        /// Gets completed achievement by player and match them by ID with database ones to create full information about earned achievement
        /// </summary>
        /// <param name="extendedCharacterModel"></param>
        /// <returns></returns>
        public static IEnumerable<AchievementsData> MatchCompletedPlayerAchievement(ExtendedCharacterModel extendedCharacterModel)
        {
            using (ComparerDatabaseContext db = new ComparerDatabaseContext())
            {
                List<AchievementsData> achievementsDatasDB = (from id in db.AchievementsData select id).ToList();

                return achievementsDatasDB.Join(extendedCharacterModel.Achievements.AchievementsCompleted.ToList(),
                                                dbAchievement => dbAchievement.ID,
                                                playerAchievement => playerAchievement,
                                                (dbAchievement, playerAchievement) => dbAchievement);
            }
        }

        /// <summary>
        /// Gets items id bonus stats parameters and try to match them to pre-prepared list of bonus stats name
        /// </summary>
        /// <param name="parsedJson"></param>
        public static void MatchItemsBonusStatistics(ExtendedCharacterModel extendedCharacterModel)
        {
            using (ComparerDatabaseContext db = new ComparerDatabaseContext())
            {
                List<IItem> itemsToMatchBonusStats = PrepereCharacterItemsStatisticsList(extendedCharacterModel.Items);
                List<BonusStats> dbBonusStats = (from data in db.BonusStats select data).ToList();

                foreach (IItem item in itemsToMatchBonusStats)
                {
                    var matchedPlayerItemStatistics = dbBonusStats.Join(item.Stats,
                                                                            dbStats => dbStats.BonusStatsID,
                                                                            x => x.Stat,
                                                                            (dbStat, stat) => new
                                                                            {
                                                                                dbStat.BonusStatsID,
                                                                                dbStat.Name,
                                                                                stat.Amount,
                                                                            }).ToList();
                }

            }
        }

        //TODO refactor this to builder pattern
        private static List<IItem> PrepereCharacterItemsStatisticsList(Items currentCharacterItems)
        {
            List<IItem> localItemsList = new List<IItem>();

            localItemsList.Add(new Head() { Stats = currentCharacterItems.Head != null ? currentCharacterItems.Head.Stats : new Stats[0] });
            localItemsList.Add(new Neck() { Stats = currentCharacterItems.Neck != null ? currentCharacterItems.Neck.Stats : new Stats[0] });
            localItemsList.Add(new Shoulder() { Stats = currentCharacterItems.Shoulder != null ? currentCharacterItems.Shoulder.Stats : new Stats[0] });
            localItemsList.Add(new Back() { Stats = currentCharacterItems.Back != null ? currentCharacterItems.Back.Stats : new Stats[0] });
            localItemsList.Add(new Chest() { Stats = currentCharacterItems.Chest != null ? currentCharacterItems.Chest.Stats : new Stats[0] });
            localItemsList.Add(new Shirt() { Stats = currentCharacterItems.Shirt != null ? currentCharacterItems.Shirt.Stats : new Stats[0] });
            localItemsList.Add(new Wrist() { Stats = currentCharacterItems.Wrist != null ? currentCharacterItems.Wrist.Stats : new Stats[0] });
            localItemsList.Add(new Hands() { Stats = currentCharacterItems.Hands != null ? currentCharacterItems.Hands.Stats : new Stats[0] });
            localItemsList.Add(new Waist() { Stats = currentCharacterItems.Waist != null ? currentCharacterItems.Waist.Stats : new Stats[0] });
            localItemsList.Add(new Legs() { Stats = currentCharacterItems.Legs != null ? currentCharacterItems.Legs.Stats : new Stats[0] });
            localItemsList.Add(new Feet() { Stats = currentCharacterItems.Feet != null ? currentCharacterItems.Feet.Stats : new Stats[0] });
            localItemsList.Add(new Finger1() { Stats = currentCharacterItems.Finger1 != null ? currentCharacterItems.Finger1.Stats : new Stats[0] });
            localItemsList.Add(new Finger2() { Stats = currentCharacterItems.Finger2 != null ? currentCharacterItems.Finger2.Stats : new Stats[0] });
            localItemsList.Add(new Trinket1() { Stats = currentCharacterItems.Trinket1 != null ? currentCharacterItems.Trinket1.Stats : new Stats[0] });
            localItemsList.Add(new Trinket2() { Stats = currentCharacterItems.Trinket2 != null ? currentCharacterItems.Trinket2.Stats : new Stats[0] });
            localItemsList.Add(new MainHand() { Stats = currentCharacterItems.MainHand != null ? currentCharacterItems.MainHand.Stats : new Stats[0] });
            localItemsList.Add(new OffHand() { Stats = currentCharacterItems.OffHand != null ? currentCharacterItems.OffHand.Stats : new Stats[0] });
            localItemsList.Add(new Tabard() { Stats = currentCharacterItems.Tabard != null ? currentCharacterItems.Tabard.Stats : new Stats[0] });

            return localItemsList;
        }
    }
}