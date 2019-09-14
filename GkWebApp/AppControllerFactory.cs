
using GkWebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GkfxWebApp
{
    public class AppControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            string connectionString =ConfigurationManager.ConnectionStrings["ApplicationDbEntities"].ConnectionString;

            var repository = new SqlServerDataAccess.SqlCurrencyRepository(connectionString);
            if (controllerType == typeof(HomeController))
            {
                return new HomeController(repository);
            }

            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}