using GkfxDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace ExchangeService
{
    public class ExchangeFetcher : IExchangeFetcher
    {
        IExchangeService _excservice;
        XDocument xdoc;
        ILogger _logger;
        public ExchangeFetcher()
        {
            _excservice = new ExchangeServiceWrapper();
        }
        public ExchangeFetcher(IExchangeService sweb, ILogger logger) //its a seam to plug in stub
        {
            //this constructor can called  by test
            _excservice = sweb;
            _logger = logger;
        }

        public IEnumerable<Currency> GetExchangeRates(string serviceUri)
        {
            IEnumerable<Currency> list = null;
            try
            {
                xdoc = _excservice.LoadXMLData(serviceUri); //can fake for logging
            }
            catch (Exception ex)
            {
                _logger.LogMessage("Null returned while reading xml file");
            }
            try
            {
                list = LoadData().ToList(); ; //can fake for logging
            }
            catch (Exception ex)
            {
                _logger.LogMessage("Couldnt parse xml document");
            }


            return list;
        }
        public IEnumerable<Currency> LoadData()
        {
            //fetch records from xDocument
            if (xdoc != null)
            {
                try
                {
                    IEnumerable<Currency> currencies = from c in xdoc.Descendants("Currency")
                                                       select new Currency
                                                       {
                                                           Unit = Int32.Parse(c.Element("Unit").Value),
                                                           CurrencyNameTr = c.Element("Isim").Value,
                                                           CurrencyName = c.Element("CurrencyName").Value,
                                                           ForexBuying = string.IsNullOrEmpty(c.Element("ForexBuying").Value) ? (decimal?)null : Decimal.Parse(c.Element("ForexBuying").Value),
                                                           ForexSelling = string.IsNullOrEmpty(c.Element("ForexSelling").Value) ? (decimal?)null : Decimal.Parse(c.Element("ForexSelling").Value),
                                                           BanknoteBuying = string.IsNullOrEmpty(c.Element("BanknoteBuying").Value) ? (decimal?)null : Decimal.Parse(c.Element("BanknoteBuying").Value),
                                                           BanknoteSelling = string.IsNullOrEmpty(c.Element("BanknoteSelling").Value) ? (decimal?)null : Decimal.Parse(c.Element("BanknoteSelling").Value),
                                                           CurrencyCode = c.Attribute("CurrencyCode").Value
                                                       };

                    return currencies;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                return null;
            }

        }

    }


}
