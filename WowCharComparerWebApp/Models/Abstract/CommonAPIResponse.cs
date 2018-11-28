using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WowCharComparerWebApp.Models.Abstract
{
    public abstract class CommonAPIResponse
    {
        public string Data { get; set; }
        public Exception Exception { get; set; } = null;
    }
}
