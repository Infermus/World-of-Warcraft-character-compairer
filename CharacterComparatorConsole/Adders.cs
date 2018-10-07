
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
            string localText = string.Empty;

            if (!TextToAdd.Equals("0"))
            {
                localText = "-" + TextToAdd + "%";
            }

            return localText + "%";
        }
    }
}
