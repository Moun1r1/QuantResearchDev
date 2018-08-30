using Moon.Data.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Finance.Risk
{
    public enum ParkingLevel
    {
        Low,
        Middle,
        High
    }
    public class Parking
    {
        public Account account { get; set; }
        public ParkingLevel exposure { get; set; }
        public Parking()
        {

        }
        internal void Analyze ()
        {


        }
    }

   
    public enum RiskDecision
    {
        Minimize,
        Avoid,
        Review
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

        //Return Modelized tree of risk factorss layers
    }
    class Classify
    {
        class RiskCategories { }
        class RiskClassifications { }

        // Return Classified Risk
    }
    class Quantify
    {
        class ProbabilityAnalysis { }
        class KeySensitiities { }
        class KeyCriticalities { }
        //Return Quantified Risks
    }
    class Grade
    {
        class RiskProbability { }
        class ImpactAnalysis { }
        
        //Return prioritized Risks
    }
    class Manage
    {
        class HistoricalRiskData { }
        class RiskManagementPlan { }

        //Return Final Action (Minimize - Avoid - Review)
    }

}
    