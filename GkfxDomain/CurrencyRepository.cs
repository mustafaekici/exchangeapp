using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkfxDomain
{
    public abstract class CurrencyRepository
    {
        public abstract IEnumerable<Currency> GetCurrencies();

        public object GetContext()
        {
            return GetDbContext();
        }

        public abstract object GetDbContext();
        public abstract void InsertExchangeRate(Currency item);
        public abstract void UpdateExchangeRate(IEnumerable<Currency> itemlist);
    }
}
