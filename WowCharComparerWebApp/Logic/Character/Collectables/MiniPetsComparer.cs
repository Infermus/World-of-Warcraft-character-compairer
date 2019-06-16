using System;
using System.Collections.Generic;
using WowCharComparerWebApp.Models;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Mappers;

namespace WowCharComparerWebApp.Logic.Character
{
    public class MiniPetsComparer
    {
        public static CharacterMiniPetsCompareResult CompareMiniPets(List<ExtendedCharacterModel> parsedResultList)
        {
            int numberOfCollected = Math.Abs(parsedResultList[0].Pets.NumberOfCollectedPets - parsedResultList[1].Pets.NumberOfCollectedPets);

            int numberOfNotCollected = Math.Abs(parsedResultList[0].Pets.NumberOfNotCollectedPets - parsedResultList[1].Pets.NumberOfNotCollectedPets);

            return new CharacterMiniPetsCompareResult()
            {
                CollectedMiniPetsDifferance = numberOfCollected,
                NotCollectedMiniPestDifferance = numberOfNotCollected
            };
        }
    }
}