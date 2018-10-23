using Autofac;
using ContosoTravel.Web.Application.Data;
using ContosoTravel.Web.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoTravel.Web.Application.IoC
{
    public class TestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SQLServerDataProvider>()
                   .As<IDataProvider>()
                   .SingleInstance();

            builder.RegisterType<Data.Mock.FlightDataMockProvider>()
                   .As<IFlightDataProvider>()
                   .SingleInstance();
        }
    }
}
