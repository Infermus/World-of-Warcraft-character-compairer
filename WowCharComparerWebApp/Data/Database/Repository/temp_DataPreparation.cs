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
                        Guid = Guid.NewGuid(),
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
                        //TODO Remove delete from
                        db.Database.ExecuteSqlCommand("DELETE FROM AchievementsData");
                        db.Database.ExecuteSqlCommand("DELETE FROM AchievementCategory");

                        for (int categoryIndex = 0; categoryIndex < apiData.AchievementCategory.Count(); categoryIndex++)
                        {
                            db.AchievementCategory.Add(new AchievementCategory()
                            {
                                ID = apiData.AchievementCategory.ElementAt(categoryIndex).ID,
                                CategoryName = apiData.AchievementCategory.ElementAt(categoryIndex).CategoryName,
                            });

                            IdentityInsertManager("AchievementCategory");

                            if (apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsData != null)
                            {
                                for (int dataIndex = 0; dataIndex < apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsData.Count(); dataIndex++)
                                {
                                    db.AchievementsData.Add(new AchievementsData()
                                    {
                                        Title = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsData.ElementAt(dataIndex).Title,
                                        ID = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsData.ElementAt(dataIndex).ID,
                                        Points = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsData.ElementAt(dataIndex).Points,
                                        Description = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsData.ElementAt(dataIndex).Description,
                                        Icon = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsData.ElementAt(dataIndex).Icon,
                                        FactionId = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsData.ElementAt(dataIndex).FactionId,
                                        AchievementCategoryID = apiData.AchievementCategory.ElementAt(categoryIndex).ID
                                    });
                                }
                                IdentityInsertManager("AchievementsData");
                            }

                            if (apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsSubCategoryData != null)
                            {
                                for (int dataIndex = 0; dataIndex < apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsSubCategoryData.Count(); dataIndex++)
                                {
                                    db.AchievementCategory.Add(new AchievementCategory()
                                    {
                                        CategoryName = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsSubCategoryData.ElementAt(dataIndex).CategoryName,
                                        ID = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsSubCategoryData.ElementAt(dataIndex).ID,
                                    });

                                    IdentityInsertManager("AchievementCategory");

                                    for (int index = 0; index < apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsSubCategoryData.ElementAt(dataIndex).AchievementsData.Count(); index++)
                                    {
                                        db.AchievementsData.Add(new AchievementsData()
                                        {
                                            Title = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsSubCategoryData.ElementAt(dataIndex).AchievementsData.ElementAt(index).Title,
                                            ID = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsSubCategoryData.ElementAt(dataIndex).AchievementsData.ElementAt(index).ID,
                                            Points = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsSubCategoryData.ElementAt(dataIndex).AchievementsData.ElementAt(index).Points,
                                            Description = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsSubCategoryData.ElementAt(dataIndex).AchievementsData.ElementAt(index).Description,
                                            Icon = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsSubCategoryData.ElementAt(dataIndex).AchievementsData.ElementAt(index).Icon,
                                            FactionId = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsSubCategoryData.ElementAt(dataIndex).AchievementsData.ElementAt(index).FactionId,
                                            AchievementCategoryID = apiData.AchievementCategory.ElementAt(categoryIndex).ID
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