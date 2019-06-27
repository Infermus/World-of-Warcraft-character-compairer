using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using WowCharComparerWebApp.Models.Achievement;

namespace WowCharComparerWebApp.Data.Database.Repository.Others
{
    public class DbAccessAchievements
    {
        private readonly ComparerDatabaseContext _comparerDatabaseContext;

        public DbAccessAchievements(ComparerDatabaseContext comparerDatabaseContext)
        {
            _comparerDatabaseContext = comparerDatabaseContext;
        }

        public IEnumerable<AchievementsData> GetAllAchievementsData()
        {
            return _comparerDatabaseContext.AchievementsData.Select(x => x);
        }

        #region refactioring

        public void InsertAllAchievementsDataFromApiRequest(Achievement apiData)
        {
            using (_comparerDatabaseContext)
            {
                using (IDbContextTransaction transaction = _comparerDatabaseContext.Database.BeginTransaction())
                {
                    try
                    {
                        //TODO Remove delete from
                        _comparerDatabaseContext.Database.ExecuteSqlCommand("DELETE FROM AchievementsData");
                        _comparerDatabaseContext.Database.ExecuteSqlCommand("DELETE FROM AchievementCategory");

                        for (int categoryIndex = 0; categoryIndex < apiData.AchievementCategory.Count(); categoryIndex++)
                        {
                            _comparerDatabaseContext.AchievementCategory.Add(new AchievementCategory()
                            {
                                ID = apiData.AchievementCategory.ElementAt(categoryIndex).ID,
                                CategoryName = apiData.AchievementCategory.ElementAt(categoryIndex).CategoryName,
                            });

                            IdentityInsertManager("AchievementCategory");

                            if (apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsData != null)
                            {
                                for (int dataIndex = 0; dataIndex < apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsData.Count(); dataIndex++)
                                {
                                    _comparerDatabaseContext.AchievementsData.Add(new AchievementsData()
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
                                    _comparerDatabaseContext.AchievementCategory.Add(new AchievementCategory()
                                    {
                                        CategoryName = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsSubCategoryData.ElementAt(dataIndex).CategoryName,
                                        ID = apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsSubCategoryData.ElementAt(dataIndex).ID,
                                    });

                                    IdentityInsertManager("AchievementCategory");

                                    for (int index = 0; index < apiData.AchievementCategory.ElementAt(categoryIndex).AchievementsSubCategoryData.ElementAt(dataIndex).AchievementsData.Count(); index++)
                                    {
                                        _comparerDatabaseContext.AchievementsData.Add(new AchievementsData()
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
                    _comparerDatabaseContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT " + tableName + " ON");
                    _comparerDatabaseContext.SaveChanges();
                    _comparerDatabaseContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT " + tableName + " OFF");
                }

                #endregion
            }
        }
    }
}
