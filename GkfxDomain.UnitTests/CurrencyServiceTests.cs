using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NSubstitute;
using System.Linq;
namespace GkfxDomain.UnitTests
{
    [TestClass]
    public class CurrencyServiceTests
    {
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "repository")]
        public void Ctor_WithNullRepository_ThrowsException()
        {
            CurrencyService cs = new CurrencyService(null);
        }

        [TestMethod]     
        public void UpdateInsertExchangeRates_OnNewData_WillInsert()
        {
            Currency cur = new Currency() { CurrencyCode = "DKK" };

            List<Currency> parseddatalist = new List<Currency>();
            parseddatalist.Add(cur);
            List<Currency> existdatalist = new List<Currency>();

            var repomock = Substitute.For<CurrencyRepository>();
            CurrencyService cs = new CurrencyService(repomock);

            repomock.When(x => x.GetCurrencies()).Do(
                x => existdatalist= new List<Currency>() {
                    new Currency() { CurrencyCode = "USD" },
                    new Currency() { CurrencyCode = "AUD"},
                    new Currency() { CurrencyCode = "EUR" }
                });
         
            repomock.When(x => x.InsertExchangeRate(Arg.Any<Currency>())).Do(
             x => existdatalist.Add(cur));

            cs.UpdateInsertExchangeRates(parseddatalist);
            Assert.AreEqual(existdatalist.Count, 4);
        
        }

        [TestMethod]
        public void UpdateInsertExchangeRates_OnExistData_WillUpdate()
        {
            Currency cur = new Currency() { CurrencyCode = "USD",Unit=100 };

            List<Currency> parseddatalist = new List<Currency>();
            //parseddatalist.Add(cur);
            List<Currency> existdatalist = new List<Currency>();

            var repomock = Substitute.For<CurrencyRepository>();
            
            repomock.When(x => x.GetCurrencies()).Do(
                x => existdatalist = new List<Currency>() {
                    new Currency() { CurrencyCode = "USD" }
                });

            repomock.When(x => x.UpdateExchangeRate(Arg.Any<IEnumerable<GkfxDomain.Currency>>())).Do(
             x => existdatalist.First().Unit=cur.Unit);

            CurrencyService cs = new CurrencyService(repomock);
            cs.UpdateInsertExchangeRates(parseddatalist);
            Assert.AreEqual(existdatalist.First().Unit, cur.Unit);
        }
    }
}
