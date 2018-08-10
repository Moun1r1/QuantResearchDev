using Microsoft.WindowsAzure.Storage.Table;
using Moon.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Moon.Resources.Management;

namespace Moon.Global
{
    public static class shared
    {
        public static ConfigGlobal Config = new ConfigGlobal();
        public static Perf Manager = new Perf();
        public static NodeUri ConfigUri = new NodeUri();
        public static bool StoreInLocalAzureDB = true;
        public static bool Running = true;
        public static CloudTable table = null;
    }
}
