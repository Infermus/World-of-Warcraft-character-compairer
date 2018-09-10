using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowCharComparerLib.Models
{
    public class Realm
    {
        public Realm(string slug, string locale)
        {
            Slug = slug;
            Locale = locale;
        }

        public string Type { get;  set; }

        public string Population { get;  set; }

        public bool Queue { get;  set; }

        public bool Status { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Battlegroup { get; set; }

        public string Locale { get; set; }

        public string Timezone { get; set;  }

        public string [] ConnectedRealms { get; set; }
    }
}