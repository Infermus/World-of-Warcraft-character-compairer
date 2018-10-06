
namespace CharacterComparatorConsole
{
    public static class Adders
    {
        public static string AddPlusToPrimaryStatPercent(string TextToAdd)
        {
            return "+" + TextToAdd + "%";
        }

        public static string AddMinusToPrimaryStatPercent(string TextToAdd)
        {
            if (!TextToAdd.Equals("0"))
            {
                return "-" + TextToAdd + "%";
            }
            return TextToAdd + "%";
        }

    }
}
