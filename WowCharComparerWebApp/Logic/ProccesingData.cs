using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using WowCharComparerWebApp.Builders;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Models;
using WowCharComparerWebApp.Models.Achievement;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.CharacterProfile.ItemsModels.Gear;
using WowCharComparerWebApp.Models.CharacterProfile.ItemsModels.Others;
using WowCharComparerWebApp.Models.Interfaces;

namespace WowCharComparerWebApp.Logic.DataResources
{
    public class ProccesingData
    {
        public static IEnumerable<AchievementsData> ComparePlayerAchievements(List<BasicCharacterModel> parsedJson)
        {
            using (ComparerDatabaseContext db = new ComparerDatabaseContext())
            {
                List<AchievementsData> achievementsDatasDB = new List<AchievementsData>();
                achievementsDatasDB = (from id in db.AchievementsData select id).ToList();

                var matchedAchievementOnPlayer = achievementsDatasDB.Join(parsedJson[parsedJson.Count - 1].Achievements.AchievementsCompleted.ToList(),
                                                                              dbAchievement => dbAchievement.ID,
                                                                              parsedJsonAchievement => parsedJsonAchievement,
                                                                              (dbAchievement, parsedJsonAchievement) => dbAchievement);
                return matchedAchievementOnPlayer.ToList();
            }
        }

        public static void ExtendItemsStatistic(List<BasicCharacterModel> parsedJson)
        {
            using (ComparerDatabaseContext db = new ComparerDatabaseContext())
            {
                List<BonusStats> statisticsIdDB = new List<BonusStats>();
                List<IItem> listOfItems = AddItemsToList(parsedJson);

                statisticsIdDB = (from data in db.BonusStats select data).ToList();
                var itemStatistics = new List<Models.CharacterProfile.ItemsModels.Others.Stats>();
                int index = 0;

                foreach (IItem statistics in listOfItems)
                {
                    itemStatistics.Add(new Models.CharacterProfile.ItemsModels.Others.Stats()
                    {
                        Amount = listOfItems[listOfItems.Count - 1].Stats[index].Amount,
                        Stat = listOfItems[listOfItems.Count - 1].Stats[index].Stat
                    });
                    index++;
                }


                var matchedPlayerItemStatistics = statisticsIdDB.Join(itemStatistics,
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

        public static List<IItem> AddItemsToList(List<BasicCharacterModel> parsedData)
        {
            for (int index = 0; index <= parsedData.Count(); index++)
            {
                Head head = ItemBuilder.BuildItem().SetAmount(parsedData.Last().Items.Head.Stats[index].Amount, parsedData.Count() - 1)
                                   .SetStat(parsedData.Last().Items.Head.Stats[index].Stat, parsedData.Count() - 1)
                                   .BuildConcerteItem<Head>();
            }

            List<IItem> itemList = new List<IItem>
            {
                new Head{ Stats = parsedData[0].Items.Head.Stats },
                new Neck{ Stats = parsedData[0].Items.Neck.Stats},

                new Back
                {
                    Stats = parsedData[0].Items.Back.Stats
                }
            };

            return itemList;
        }
    }
}