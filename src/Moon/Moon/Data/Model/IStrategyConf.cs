using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Model
{
    public enum StrategyConf
    {
        FromFile,
        FromNode,
        FromNN,
        FromGA,
        FromAssembly
    }
    public interface IStrategyConf :IRoot
    {
         DateTime GenerationDate { get; set; }
         StrategyConf Source { get; set; }

    }
}
