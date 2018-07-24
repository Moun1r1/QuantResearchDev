using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Model
{
    public class FactorDefintion
    {
        public int weight { get; set; } = 0;
        public int colreationscore { get; set; } = 0;
        public int volumeretrace { get; set; } = 0;
        public int followerscore { get; set; } = 0;
        public int indexonmarkets { get; set; } = 0;

        public FactorDefintion()
        {

        }
    }
}
