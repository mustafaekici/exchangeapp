using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExchangeService;
using GkfxDomain;
using System.Collections.Generic;
using NSubstitute;

namespace ExchangeService.UnitTests
{
    [TestClass]
    public class ExchangeFetcherTests
    {
        [TestMethod]
        public void GetExchangeRates_IsValidServiceUri_ReturnData()
        {
            ExchangeFetcher fetcher = new ExchangeFetcher();
            Assert.IsNotNull(fetcher.GetExchangeRates("http://www.tcmb.gov.tr/kurlar/today.xml"));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetExchangeRates_IsNotValidServiceUri_ThrowsException()
        {
            ExchangeFetcher fetcher = new ExchangeFetcher();
            fetcher.GetExchangeRates(string.Empty);
        }
        [TestMethod]
        public void LoadData_IsNotValidServiceUri_ReturnNull()
        {
            ExchangeFetcher fetcher = new ExchangeFetcher();
            Assert.IsNull(fetcher.LoadData());
        }

        [TestMethod]
        public void GetExchangeRates_WrongParsingXml_ThrowException()
        {
            var fetcher = Substitute.For<IExchangeService>();
            var logger = Substitute.For<ILogger>();
            ExchangeFetcher ex = new ExchangeFetcher(fetcher, logger);
            ex.GetExchangeRates("<MyTestElement/>");

            logger.Received().LogMessage("Couldnt parse xml document");
        }

        [TestMethod]
        public void GetExchangeRates_IsNotValidServiceUriForLoadingXml_LogExceptionForXML()
        {
            var fakefetcher = Substitute.For<IExchangeService>();
            var logger = Substitute.For<ILogger>();
            
            fakefetcher.When(x => x.LoadXMLData(Arg.Any<string>())).Do(
               info => { throw new Exception("fake exception"); });

            ExchangeFetcher ex = new ExchangeFetcher(fakefetcher,logger);
            ex.GetExchangeRates("http://www.tcmb.gov.tr/kurlar/today.xml");
            logger.Received().LogMessage("Null returned while reading xml file");

        }

        
    }

}
