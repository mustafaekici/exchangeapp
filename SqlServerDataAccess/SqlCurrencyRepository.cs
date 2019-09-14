using GkfxDomain;
using SqlServerDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerDataAccess
{
    public class SqlCurrencyRepository : CurrencyRepository, IDisposable
    {
        public CurrencyDbContext context;
        public SqlCurrencyRepository(string connString)
        {
            this.context = new CurrencyDbContext(connString);

        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();

                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override IEnumerable<GkfxDomain.Currency> GetCurrencies()
        {
            var cu = (from p in this.context.Currencies select p).AsEnumerable();
            return from p in cu select p.ToDomainCurrency();
        }

        public override object GetDbContext()
        {
            return context;
        }

        public override void InsertExchangeRate(GkfxDomain.Currency item)
        {
            context.Currencies.Add(new Models.Currency()
            {
                BanknoteBuying = item.BanknoteBuying,
                BanknoteSelling = item.BanknoteSelling,
                CurrencyCode = item.CurrencyCode,
                CurrencyName = item.CurrencyName,
                CurrencyNameTr = item.CurrencyNameTr,
                ForexBuying = item.ForexBuying,
                ForexSelling = item.ForexSelling,
                Unit = item.Unit
            });

            context.SaveChanges();
        }

        public override void UpdateExchangeRate(IEnumerable<GkfxDomain.Currency> itemlist)
        {
            foreach (var item in itemlist)
            {
                var curr = context.Currencies.SingleOrDefault(x => x.CurrencyCode == item.CurrencyCode);
                if (curr == null)
                {
                    //log or throw exception
                }
                else
                {
                    curr.BanknoteBuying = item.BanknoteBuying;
                    curr.BanknoteSelling = item.BanknoteSelling;
                    curr.CurrencyName = item.CurrencyName;
                    curr.CurrencyNameTr = item.CurrencyNameTr;
                    curr.ForexBuying = item.ForexBuying;
                    curr.ForexSelling = item.ForexSelling;
                    curr.Unit = item.Unit;
                }
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    //log or throw exception
                }
            }

        }
    }
}
