
using System.ComponentModel.DataAnnotations;

namespace WowCharComparerWebApp.Models.Achievement
{
    public class Criteria
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public int OrderIndex { get; set; }

        public int Max { get; set; }
    }
}
