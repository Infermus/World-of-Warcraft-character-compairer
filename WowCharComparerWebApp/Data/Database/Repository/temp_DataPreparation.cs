using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Models;
using WowCharComparerWebApp.Models.Achievement;

namespace WowCharComparerWebApp.Data.Database.Repository
{
    public static class temp_DataPreparation
    {
        public static void InsertBonusStatsTableFromJsonFile()
        {
            var parsedJsonData = JsonProcessing.GetDataFromJsonFile<Models.Statistics.Statistics>(@"\Statistics.json");

            using (var db = new ComparerDatabaseContext())
            {
                for (int index = 0; index < parsedJsonData.BonusStats.Length; index++)
                {
                    db.BonusStats.Add(new BonusStats()
                    {
                        ID = Guid.NewGuid(),
                        BonusStatsID = parsedJsonData.BonusStats[index].BonusStatsID,
                        Name = parsedJsonData.BonusStats[index].Name
                    });
                }
                db.SaveChanges();
            }
        }

        public static void InsertAllAchievementsDataFromApiRequest(Achievement apiData)
        {
            using (ComparerDatabaseContext db = new ComparerDatabaseContext())
            {
                using (IDbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand("DELETE FROM AchievementCategory");
                        db.Database.ExecuteSqlCommand("DELETE FROM AchievementsData");

                        for (int categoryIndex = 0; categoryIndex < apiData.AchievementCategory.Length; categoryIndex++)
                        {
                            db.AchievementCategory.Add(new AchievementCategory()
                            {
                                ID = apiData.AchievementCategory[categoryIndex].ID,
                                CategoryName = apiData.AchievementCategory[categoryIndex].CategoryName,
                            });

                            IdentityInsertManager("AchievementCategory");

                            if (apiData.AchievementCategory[categoryIndex].AchievementsData != null)
                            {
                                for (int dataIndex = 0; dataIndex < apiData.AchievementCategory[categoryIndex].AchievementsData.Length; dataIndex++)
                                {
                                    db.AchievementsData.Add(new AchievementsData()
                                    {
                                        Title = apiData.AchievementCategory[categoryIndex].AchievementsData[dataIndex].Title,
                                        ID = apiData.AchievementCategory[categoryIndex].AchievementsData[dataIndex].ID,
                                        Points = apiData.AchievementCategory[categoryIndex].AchievementsData[dataIndex].Points,
                                        Description = apiData.AchievementCategory[categoryIndex].AchievementsData[dataIndex].Description,
                                        Icon = apiData.AchievementCategory[categoryIndex].AchievementsData[dataIndex].Icon,
                                        FactionId = apiData.AchievementCategory[categoryIndex].AchievementsData[dataIndex].FactionId,
                                    });
                                }
                                IdentityInsertManager("AchievementsData");
                            }

                            if (apiData.AchievementCategory[categoryIndex].AchievementsSubCategoryData != null)
                            {
                                for (int dataIndex = 0; dataIndex < apiData.AchievementCategory[categoryIndex].AchievementsSubCategoryData.Length; dataIndex++)
                                {
                                    db.AchievementCategory.Add(new AchievementCategory()
                                    {
                                        CategoryName = apiData.AchievementCategory[categoryIndex].AchievementsSubCategoryData[dataIndex].CategoryName,
                                        ID = apiData.AchievementCategory[categoryIndex].AchievementsSubCategoryData[dataIndex].ID,
                                    });

                                    IdentityInsertManager("AchievementCategory");

                                    for (int index = 0; index < apiData.AchievementCategory[categoryIndex].AchievementsSubCategoryData[dataIndex].AchievementsData.Length; index++)
                                    {
                                        db.AchievementsData.Add(new AchievementsData()
                                        {
                                            Title = apiData.AchievementCategory[categoryIndex].AchievementsSubCategoryData[dataIndex].AchievementsData[index].Title,
                                            ID = apiData.AchievementCategory[categoryIndex].AchievementsSubCategoryData[dataIndex].AchievementsData[index].ID,
                                            Points = apiData.AchievementCategory[categoryIndex].AchievementsSubCategoryData[dataIndex].AchievementsData[index].Points,
                                            Description = apiData.AchievementCategory[categoryIndex].AchievementsSubCategoryData[dataIndex].AchievementsData[index].Description,
                                            Icon = apiData.AchievementCategory[categoryIndex].AchievementsSubCategoryData[dataIndex].AchievementsData[index].Icon,
                                            FactionId = apiData.AchievementCategory[categoryIndex].AchievementsSubCategoryData[dataIndex].AchievementsData[index].FactionId,
                                        });

                                        IdentityInsertManager("AchievementsData");
                                    }
                                }
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }

               void IdentityInsertManager(string tableName)
               {
                    db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT " + tableName + " ON");
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT " + tableName + " OFF");
                }
            }
        }
    }
}