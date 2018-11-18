using System.Collections.Generic;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Mappers;

namespace WowCharComparerWebApp.Logic.Character
{
    public class MiniPetsComparer
    {
        public static CharacterMiniPetsCompareResult CompareMiniPets(List<CharacterModel> parsedResultList)
        {
            int numberOfCollected = parsedResultList[0].Pets.NumberOfCollectedPets > parsedResultList[1].Pets.NumberOfCollectedPets
                                       ? parsedResultList[0].Pets.NumberOfCollectedPets - parsedResultList[1].Pets.NumberOfCollectedPets
                                       : parsedResultList[1].Pets.NumberOfCollectedPets - parsedResultList[0].Pets.NumberOfCollectedPets;

            int numberOfNotCollected = parsedResultList[0].Pets.NumberOfNotCollectedPets > parsedResultList[1].Pets.NumberOfNotCollectedPets
                                       ? parsedResultList[0].Pets.NumberOfNotCollectedPets - parsedResultList[1].Pets.NumberOfNotCollectedPets
                                       : parsedResultList[1].Pets.NumberOfNotCollectedPets - parsedResultList[0].Pets.NumberOfNotCollectedPets;

            return new CharacterMiniPetsCompareResult()
            {
                CollectedMiniPetsDifferance = numberOfCollected,
                NotCollectedMiniPestDifferance = numberOfNotCollected
            };
        }

    }
}