using System.Collections.Generic;
using WowCharComparerWebApp.Logic.Character.Statistics;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Mappers;
using Xunit;

namespace WowCharComparerWebApp.Tests.Logic.Character.Statistics
{
    // Convention
    // method name : UnitOfWork_StateUnderTest_ExpectedBehavior
    // 1. Arrange
    // 2. Act
    // 3. Assert

    public class StatisticsComparerTest
    {
        [Fact]
        public void PlayerStatisticsComparer_WhenCharacterStatisticAreEquals_ShouldReturnSameValue()
        {
            //Arrange
            CharacterStatisticsCompareResult expected = new CharacterStatisticsCompareResult
            {
                CharacterCompareStrDifference = "0%",
                CharacterCompareIntDifference = "0%",
                CharacterCompareAgiDifference = "0%",
                CharacterCompareStaDifference = "0%"
            };

            List<ExtendedCharacterModel> extendedCharacterModels = new List<ExtendedCharacterModel>();
            //Act
            {
                for (int i = 0; i <= 1; i++)
                {
                    extendedCharacterModels.Add(new ExtendedCharacterModel()
                    {
                        Stats = new Stats()
                        {
                            Agi = 50,
                            Int = 50,
                            Sta = 50,
                            Str = 50,
                        }
                    });
                }              
            }
            CharacterStatisticsCompareResult result = new StatisticsComparer().CompareCharacterStatistics(extendedCharacterModels);

            //Assert
            Assert.Equal(result.CharacterCompareAgiDifference, expected.CharacterCompareAgiDifference);
            Assert.Equal(result.CharacterCompareIntDifference, expected.CharacterCompareIntDifference);
            Assert.Equal(result.CharacterCompareStaDifference, expected.CharacterCompareStaDifference);
            Assert.Equal(result.CharacterCompareStrDifference, expected.CharacterCompareStrDifference);
        }
    }
}