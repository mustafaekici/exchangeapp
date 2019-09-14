using GkfxDomain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExchangeService
{
    public class ExchangeServiceWrapper : IExchangeService
    {
 
        public XDocument LoadXMLData(string serviceuri)
        {
            XDocument mXDocument = null;
            try
            {

                //load xml document
                mXDocument = XDocument.Load(serviceuri);

                if (mXDocument == null)
                {
                    throw new Exception("Null returned while reading xml file");
                }
            }
            catch (Exception ex)
            {
                //Exception management code
            }
            return mXDocument;
        }



    }


}
