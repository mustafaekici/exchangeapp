using GkfxDomain;
using GkWebApp.Controllers;
using Microsoft.Practices.Unity;
using SqlServerDataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GkWebApp.Unity
{
    public class AppContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationDbEntities"].ConnectionString;
            var sqlCtorParam = new InjectionConstructor(connectionString);

            if (!String.IsNullOrEmpty(connectionString))
                SqlDependency.Start(connectionString);

            this.Container.RegisterType<CurrencyRepository, SqlCurrencyRepository>(new PerResolveLifetimeManager(), sqlCtorParam);
            //other repositories

            this.Container.RegisterType<HomeController>(new InjectionConstructor());
        }
    }
}