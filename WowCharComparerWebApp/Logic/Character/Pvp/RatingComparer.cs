using System;
using System.Collections.Generic;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.Mappers;

namespace WowCharComparerWebApp.Logic.Character.Pvp
{
    public class RatingComparer
    {
        public static CharacterArenaRatingsCompareResult CompareRating(List<CharacterModel> parsedResultList)
        {
            int bracket2s = Math.Abs(parsedResultList[0].Pvp.Brackets.Arena_Bracket_2v2.Rating - parsedResultList[1].Pvp.Brackets.Arena_Bracket_2v2.Rating);

            int bracket3s = Math.Abs(parsedResultList[0].Pvp.Brackets.Arena_Bracket_3v3.Rating - parsedResultList[1].Pvp.Brackets.Arena_Bracket_3v3.Rating);

            int rbgRating = Math.Abs(parsedResultList[0].Pvp.Brackets.Arena_Bracket_RBG.Rating - parsedResultList[1].Pvp.Brackets.Arena_Bracket_RBG.Rating);

            return new CharacterArenaRatingsCompareResult()
            {
                ArenaBracket2v2RatingResult = bracket2s,
                ArenaBracket3v3RatingResult = bracket3s,
                RandomBattlegroundRatingResult = rbgRating
            };
        }
    }
}
