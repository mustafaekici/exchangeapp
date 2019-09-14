using ExchangeService;
using GkfxDomain;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateFetcher
{
    public class FetcherJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Fetched Exchange Rates : "+ DateTime.Now.ToString());
            FetchRates();
            
        }
        public void FetchRates()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationDbEntities"].ConnectionString;
            SqlServerDataAccess.SqlCurrencyRepository repository = new SqlServerDataAccess.SqlCurrencyRepository(connectionString);
            string serviceUri = "http://www.tcmb.gov.tr/kurlar/today.xml";

            ExchangeFetcher fetcher = new ExchangeFetcher();
            var parsedlist=fetcher.GetExchangeRates(serviceUri);

            CurrencyService cs = new GkfxDomain.CurrencyService(repository);
            cs.UpdateInsertExchangeRates(parsedlist);

            Console.WriteLine("Exchange Rates Populated to Database : " + DateTime.Now.ToString());
        }
    }
}
