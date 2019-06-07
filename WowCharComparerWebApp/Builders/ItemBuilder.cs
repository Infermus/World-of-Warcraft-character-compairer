using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowCharComparerWebApp.Models.CharacterProfile;
using WowCharComparerWebApp.Models.CharacterProfile.ItemsModels.Gear;

namespace WowCharComparerWebApp.Builders
{
    public static class ItemBuilder
    {
        private static DefaultItem defaultItem;

        public static DefaultItem BuildItem()
        {
            defaultItem = new DefaultItem();
            return defaultItem;
        }

        public static DefaultItem SetAmount(this DefaultItem defaultItem, int amount, int indexOfCharacter)
        {
            if (indexOfCharacter <= 1)
                defaultItem.Stats[indexOfCharacter].Amount = amount;
            else
                throw new NullReferenceException();

            return defaultItem;
        }

        public static DefaultItem SetStat(this DefaultItem defaultItem, int stat, int indexOfCharacter)
        {
            if (indexOfCharacter <= 1)
                defaultItem.Stats[indexOfCharacter].Stat = stat;
            else
                throw new NullReferenceException();

            return defaultItem;
        }

        public static T BuildConcerteItem<T>(this DefaultItem defaultItem) where T: DefaultItem
        {
            if (defaultItem != null)
                return (T)defaultItem;
            else throw new Exception();
        }
    }
}
