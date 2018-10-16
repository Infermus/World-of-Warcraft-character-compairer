
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
    }
}
