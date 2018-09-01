using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Model
{
   public interface IRoot
    {
        string Jscontainer { get; set; }
        void Update();
        string TypeOfData { get; set; }
    }
}
