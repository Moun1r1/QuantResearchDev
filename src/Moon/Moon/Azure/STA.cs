using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Azure
{
    public class STA
    {
        public string BaseModel { get; set; } = "c4c7ab2a-c85a-4cfb-944a-d4c265d4447c";
        public string Name { get; set; } = "Root";
        public string TargetTable { get; set; }
        public STA()
        {

        }
    }

    public class TableRoute
    {
        public string Name { get; set; }
        public string SourceClass { get; set; }
        public string TargetTable { get; set; }
        public bool UsePropertiesList { get; set; } = false;


        public TableRoute()
        {

        }

    }
}
