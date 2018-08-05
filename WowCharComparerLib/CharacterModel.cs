using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowCharComparerLib
{
    class CharacterModel
    {
        public long LastModified { get; set; }
        public string Name { get; set; }
        public string[] Items { get; set; }
    }
}