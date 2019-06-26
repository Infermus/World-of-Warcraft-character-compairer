using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowCharComparerWebApp.Data.Helpers;
using WowCharComparerWebApp.Models;

namespace WowCharComparerWebApp.Data.Database.Repository.Others
{
    public class DbAccessBonusStats
    {
        private ComparerDatabaseContext _comparerDatabaseContext;

        public DbAccessBonusStats(ComparerDatabaseContext comparerDatabaseContext)
        {
            _comparerDatabaseContext = comparerDatabaseContext;
        }

        public IEnumerable<BonusStats> GetAllBonusStats()
        {
            return _comparerDatabaseContext.BonusStats.Select(x => x);
        }

        public void InsertBonusStatsTableFromJsonFile()
        {
            var parsedJsonData = JsonProcessing.GetDataFromJsonFile<Models.Statistics.Statistics>(@"\Statistics.json");

            using (_comparerDatabaseContext)
            {
                for (int index = 0; index < parsedJsonData.BonusStats.Length; index++)
                {
                    _comparerDatabaseContext.BonusStats.Add(new BonusStats()
                    {
                        ID = Guid.NewGuid(),
                        BonusStatsID = parsedJsonData.BonusStats[index].BonusStatsID,
                        Name = parsedJsonData.BonusStats[index].Name
                    });
                }
                _comparerDatabaseContext.SaveChanges();
            }
        }
    }
}
