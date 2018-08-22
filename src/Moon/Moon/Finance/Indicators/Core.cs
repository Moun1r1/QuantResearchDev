using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Finance.Indicators
{
   public class Core
    {
        public List<Type> ClassBase { get; set; }
        public Moon.Data.Provider.Core provider { get; set; }
        public List<RootTA> TA { get; set; }
        public Core()
        {
            ClassBase.Add(new RSI().GetType());
            TA.Add(new RSI());

        }
        public void LinkMeToo(Moon.Data.Provider.Core core)
        {

        }
    }
}
