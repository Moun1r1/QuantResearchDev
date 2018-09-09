using Moon.Data.Extender;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class StrategyConfig : IRoot, IStrategyConf , INotifyPropertyChanged
    {
        private StrategyConf source = StrategyConf.FromAssembly;
        public string Jscontainer { get; set; }
        public string TypeOfData { get; set; } = "Strategy Config";
        public DateTime GenerationDate { get; set; } = DateTime.Now;
        public StrategyConf Source {
            get { return this.source; }
            set
            {
                if(value != this.source)
                {
                    this.source = value;
                    NotifyPropertyChanged();
                }
            }

        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public StrategyConfig()
        {
            this.Jscontainer = Newtonsoft.Json.JsonConvert.SerializeObject(this);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Update()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return this.ToJson();
        }

    }
}
