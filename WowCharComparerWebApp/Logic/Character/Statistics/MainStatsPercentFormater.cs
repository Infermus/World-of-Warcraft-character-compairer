namespace WowCharComparerWebApp.Logic.Character.Statistics
{
    public static class MainStatsPercentFormater
    {
        public static string AddPlusToPrimaryStatPercent(string textToAdd)
        {
            return "+" + textToAdd + "%";
        }

        public static string AddMinusToPrimaryStatPercent(string textToAdd)
        {
            return textToAdd.Equals("0") ? textToAdd + "%" : "-" + textToAdd + "%";
        }
    }
}
