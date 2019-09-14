using GkfxDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeService
{
    public interface IExchangeFetcher
    {
        IEnumerable<Currency> LoadData();
    }
}
