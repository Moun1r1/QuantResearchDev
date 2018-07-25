using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Finance.Risk
{
    class Core
    {
    }

   

    class Identity
    {
        class Objectives
        {
            public RiskGroups Group { get; set; }
            public RiskTable Table { get; set; }
            public Experiences Exp { get; set; }
            public WBS WorkBreakDownStructure { get; set; }

        }
        class RiskGroups { }
        class RiskTable { }
        class Experiences { }
        class WBS {
            public string[] DirectL1 { get; set; }
            public string[] DirectL3 { get; set; }
            public string[] DirectL4 { get; set; }
            public string[] DirectL5 { get; set; }
            public bool MatchTree { get; set; }
            public bool HasDirectTimeImpact { get; set; }
            public TimeSpan RoT { get; set; }
        }


    }
    class Classify
    {

    }
    class Quantify
    {

    }
    class Grade
    {

    }
    class Manage
    {

    }
}
    