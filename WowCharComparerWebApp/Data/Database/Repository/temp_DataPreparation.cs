using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Models;

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
                        Id = Guid.NewGuid(),
                        StatisticId = parsedJsonData.BonusStats[index].StatisticId,
                        Name = parsedJsonData.BonusStats[index].Name
                    });
                }
                db.SaveChanges();
            }
        }
    }
}