using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowCharComparerLib.Models
{
    public class BlizzardAPIResponse
    {
        public string Data { get; set; }
        public Exception Exception { get; set; } = null;
    }
}