using System;
namespace WowCharComparerLib.Models
{
    public class BlizzardAPIResponse
    {
        public string Data { get; set; }
        public Exception Exception { get; set; } = null;
    }
}