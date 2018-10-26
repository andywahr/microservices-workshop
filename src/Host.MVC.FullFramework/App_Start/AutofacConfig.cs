﻿using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
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
            builder.RegisterAssemblyModules(thisAssembly, typeof(Application.Features).Assembly);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}