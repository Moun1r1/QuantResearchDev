using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Finance.Model.InvestmentManagement
{

        class PortofilioModelling {

        // at least 3 currency not directly linked for passing an order (ETH/BTC/BNB ??)
        public int cashflow_distribution = 5;
        // at least x2 volume of movement on current pair
        public int activity_median_volume_move = 2;
        // allow portofilio pairs balances staying on overall downtrend
        public bool allow_neg_keypairs = false;


         internal bool IsAllowedToPark(string pairname)
        {
            return true;
        }



        }
        class InvestmentDecisions { }
        class StrategicAdvice { }
        class PortofilioConstruction { }

}
