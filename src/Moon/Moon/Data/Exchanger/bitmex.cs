using Moon.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Exchanger
{
    public class BitmexExchanger : IRoot, IExchanger
    {
        public Exchange Platform { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Provider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime UpateSince { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime StartedSince { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Jscontainer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TypeOfData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Close()
        {
            throw new NotImplementedException();
        }

        public bool Connect()
        {
            throw new NotImplementedException();
        }

        public bool GetByTime(DateTime Start, DateTime End, string symbol)
        {
            throw new NotImplementedException();
        }

        public bool Init()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        bool IExchanger.Update()
        {
            throw new NotImplementedException();
        }
    }
}
