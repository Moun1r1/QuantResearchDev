using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Model
{
   public interface Root
    {
        string Jscontainer { get; set; }
        void Update();
    }
}
