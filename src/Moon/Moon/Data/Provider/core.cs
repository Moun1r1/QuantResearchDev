using Moon.Data.Exchanger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Provider
{
    class Core
    {
        public binance bclient { get; set; } = new binance();
        public Core()
        {

        }
    }
}
