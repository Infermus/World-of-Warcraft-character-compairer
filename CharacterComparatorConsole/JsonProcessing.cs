
using System.Collections.Generic;
using System.IO;
using WowCharComparerWebApp.Models.Statistics;

namespace CharacterComparatorConsole
{
   public static class JsonProcessing
    {
        public static Statistics GetDataFromJsonFile(string fileName)
        {
            using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + fileName))
            {
                string json = reader.ReadLine();
                var parsedResult = WowCharComparerWebApp.Data.Helpers.ResponseResultFormater.DeserializeJsonData<WowCharComparerWebApp.Models.Statistics.Statistics>(json);
                return parsedResult;
            }
        }

        public static Dictionary<int,string> AddDataToDictionary(Statistics jsonData  )
        {
            Dictionary<int, string> statisticDictionary = new Dictionary<int, string>();
            for (int index = 0; index < jsonData.BonusStats.Length; index++)
            {
                statisticDictionary.Add(jsonData.BonusStats[index].Id, jsonData.BonusStats[index].Name);
                //TODO There is a problem with names, versatility peforming two times 
                //Check if both are needed, indexes: 35,62
            }
            return statisticDictionary;
        }
    }
}
