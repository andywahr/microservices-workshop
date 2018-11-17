using Autofac;
using Autofac.Integration.Mvc;
using ContosoTravel.Web.Application;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoTravel.Web.Host.MVC.FullFramework
{
    public class AutofacConfig
    {
        public static void RegisterContainer()
        {
            var thisAssembly = typeof(MvcApplication).Assembly;
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterControllers(thisAssembly);
            Setup.InitCotoso(ConfigurationManager.AppSettings["KeyVaultUrl"], System.Web.Hosting.HostingEnvironment.MapPath("~/"), thisAssembly, builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}