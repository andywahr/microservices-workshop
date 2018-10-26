using ContosoTravel.Web.Application;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ContosoTravel.Web.Host.MVC.FullFramework
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Features.DataType = (DataType)Enum.Parse(typeof(DataType), ConfigurationManager.AppSettings["DataType"]);
            Features.ServicesType = (ServicesType)Enum.Parse(typeof(ServicesType), ConfigurationManager.AppSettings["ServicesType"]);
            Web.Application.Models.SiteModel.SiteTitle = "Contoso Travel - .Net Framework";
            AutofacConfig.RegisterContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
