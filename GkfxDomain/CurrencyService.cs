using GkfxDomain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkfxDomain
{
    public class CurrencyService
    {
        private readonly CurrencyRepository repository;

        public CurrencyService(CurrencyRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
        }

        public IEnumerable<Currency> GetAll()
        {
            return this.repository.GetCurrencies();
        }

        public void UpdateInsertExchangeRates(IEnumerable<Currency> collection)
        {
            var comparer = new ExchangeRateEqualityComparer();
            var exists = from c in GetAll()
                         where c.CurrencyCode != null
                         select c;

            var added = collection.Except(exists, comparer).ToList();
            if (added != null)
            {
                foreach (var item in added)
                {
                    if (!string.IsNullOrEmpty(item.CurrencyCode))
                    {
                        if (item.CurrencyId <= 0)
                        {
                            repository.InsertExchangeRate(item);
                        }
                    }
                }
            }
            if (added.Count == 0)
            {
                repository.UpdateExchangeRate(collection);
            }
        }

    }
}
