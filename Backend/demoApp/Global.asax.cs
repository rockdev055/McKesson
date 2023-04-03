using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using demoApp.Services;
using Ninject;
using Ninject.Web.WebApi;
using NLog;

namespace demoApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Configure NLog
            //LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(Server.MapPath("~/NLog.config"), true);
            LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(Server.MapPath("~/NLog.config"));
            LogManager.ThrowConfigExceptions = true;
        }
    }
}
