using System;
using System.Collections.Generic;
using System.Linq;
using WowCharComparerWebApp.Models.Mappers;
using WowCharComparerWebApp.ViewModel.CharacterProfile;

namespace WowCharComparerWebApp.Logic.Character
{
    public class MiniPetsComparer
    {
        public CharacterMiniPetsCompareResult CompareMiniPets(List<ProcessedCharacterViewModel> parsedResultList)
        {
            var result = new CharacterMiniPetsCompareResult();

            if (parsedResultList.Any(x => x.Pets != null))
            {
                result.CollectedMiniPetsDifferance = Math.Abs(parsedResultList[0].Pets.NumberOfCollectedPets - parsedResultList[1].Pets.NumberOfCollectedPets);
                result.NotCollectedMiniPestDifferance = Math.Abs(parsedResultList[0].Pets.NumberOfNotCollectedPets - parsedResultList[1].Pets.NumberOfNotCollectedPets);
            }

            return result;
        }
    }
}