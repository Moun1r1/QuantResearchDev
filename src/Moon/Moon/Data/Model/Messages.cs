using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Model
{
   public enum TypeOfContent
    {
        Unknown,
        Candles,
        Trades
    }
   public class Messages
    {
        public string ContentNodeName { get; set; } = Environment.MachineName;
        public TypeOfContent MessageType { get; set; } = TypeOfContent.Candles;
        public string Content { get; set; } = string.Empty;
        public string RootType { get; set; }
        public string TargetObject { get; set; }
        public Messages()
        {

        }
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
