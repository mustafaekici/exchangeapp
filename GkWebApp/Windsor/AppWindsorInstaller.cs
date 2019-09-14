using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GkWebApp.Controllers;
using SqlServerDataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GkWebApp.Windsor
{
    public class AppWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container,
            IConfigurationStore store)
        {
            container.Register(AllTypes
                .FromAssemblyContaining<HomeController>()
                .BasedOn<IController>()
                .Configure(r => r.LifeStyle.PerWebRequest));

            //Register types..

            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationDbEntities"].ConnectionString;
            container.Register(AllTypes
                .FromAssemblyContaining<SqlCurrencyRepository>()
                .Where(t => t.Name.StartsWith("Sql"))
                .WithService
                .Select((t, b) => new[] { t.BaseType })
                .Configure(r => r.LifeStyle.PerWebRequest
                    .DependsOn((new
                    {
                        connString = connectionString
                    }))));

            if (!String.IsNullOrEmpty(connectionString))
                SqlDependency.Start(connectionString);
        }
    }
}