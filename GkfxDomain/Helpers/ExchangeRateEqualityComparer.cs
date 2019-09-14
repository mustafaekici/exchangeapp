using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkfxDomain.Helpers
{
    public class ExchangeRateEqualityComparer : IEqualityComparer<Currency>
    {
        public int GetHashCode(Currency obj)
        {
            return (obj == null) ? 0 : obj.CurrencyCode.GetHashCode();
        }

        public bool Equals(Currency x, Currency y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;
            return x.CurrencyCode == y.CurrencyCode;
        }
    }
}
