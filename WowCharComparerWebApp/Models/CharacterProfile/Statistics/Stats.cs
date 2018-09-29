
namespace WowCharComparerWebApp.Models.CharacterProfile.Statistics
{
    public class Stats : Model
    {
        public int Quantity { get; set; }

        public long LastUpdated { get; set; }

        public bool Money { get; set; }
    }
}
