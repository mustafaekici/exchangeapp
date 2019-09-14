using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using GkfxWebApp;
using GkWebApp.Unity;
using GkWebApp.Windsor;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GkWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        #region Poor Man's DI
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationDbEntities"].ConnectionString;

            if (!String.IsNullOrEmpty(connectionString))
                SqlDependency.Start(connectionString);

            var controllerFactory = new AppControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        #endregion

        #region Castle Windsor, based on web.config
        /* Uncomment this section and comment out the current Application_Start to use this
         * configuration */
        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();
        //    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);

        //    var container = new WindsorContainer(new XmlInterpreter());
        //    var controllerFactory = container.Resolve<IControllerFactory>();

        //    ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        //}
        #endregion

        #region Castle Windsor, convention-based
        /* Uncomment this section and comment out the current Application_Start to use this
         * configuration */
        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();
        //    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);

        //    var container = new WindsorContainer();
        //    container.Install(new AppWindsorInstaller());

        //    var controllerFactory = new WindsorControllerFactory(container);

        //    ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        //}
        #endregion

        #region Unity, Code as Configuration
        /* Uncomment this section and comment out the current Application_Start to use this
         * configuration */
        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();
        //    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);

        //    var container = new UnityContainer();
        //    container.AddNewExtension<AppContainerExtension>();

        //    var controllerFactory =new UnityControllerFactory(container);

        //    ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        //}
        #endregion

        #region Unity, based on web.config
        /* Uncomment this section and comment out the current Application_Start to use this
         * configuration */
        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();
        //    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);

        //    var container = new UnityContainer();
        //    container.LoadConfiguration();

        //    var controllerFactory =new UnityControllerFactory(container);

        //    ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        //}

        #endregion
    }
}
