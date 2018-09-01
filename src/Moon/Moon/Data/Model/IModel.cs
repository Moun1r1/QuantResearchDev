using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Model
{
    public interface IModel : IRoot
    {
        string ModelName { get; set; }
        string ModelDomain { get; set; }
        string ModelId { get; set; }
        DateTime LastModelChange { get; set; }


    }
}
