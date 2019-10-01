using System;
using System.Collections.Generic;
using System.Linq;
using WowCharComparerWebApp.Models.Mappers;
using WowCharComparerWebApp.ViewModel.CharacterProfile;

namespace WowCharComparerWebApp.Logic.Character.Pvp
{
    public class RatingComparer
    {
        public CharacterArenaRatingsCompareResult CompareRating(List<ProcessedCharacterViewModel> parsedResultList)
        {
            var result = new CharacterArenaRatingsCompareResult();

            if (parsedResultList.All(x => x.Pvp != null) && parsedResultList.All(x => x.Pvp.Brackets != null))
            {
                if(parsedResultList[0].Pvp.Brackets.Arena_Bracket_2v2 != null && parsedResultList[1].Pvp.Brackets.Arena_Bracket_2v2 != null)
                    result.ArenaBracket2v2RatingResult = Math.Abs(parsedResultList[0].Pvp.Brackets.Arena_Bracket_2v2.Rating - parsedResultList[1].Pvp.Brackets.Arena_Bracket_2v2.Rating);

                if (parsedResultList[0].Pvp.Brackets.Arena_Bracket_3v3 != null && parsedResultList[1].Pvp.Brackets.Arena_Bracket_3v3 != null)
                    result.ArenaBracket3v3RatingResult = Math.Abs(parsedResultList[0].Pvp.Brackets.Arena_Bracket_3v3.Rating - parsedResultList[1].Pvp.Brackets.Arena_Bracket_3v3.Rating);

                if (parsedResultList[0].Pvp.Brackets.Arena_Bracket_RBG != null && parsedResultList[1].Pvp.Brackets.Arena_Bracket_RBG != null)
                    result.RandomBattlegroundRatingResult = Math.Abs(parsedResultList[0].Pvp.Brackets.Arena_Bracket_RBG.Rating - parsedResultList[1].Pvp.Brackets.Arena_Bracket_RBG.Rating);
            }

            return result;
        }
    }
}
