using GkfxDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExchangeService
{
    public interface IExchangeService
    {
        XDocument LoadXMLData(string serviceuri);
    }
}
