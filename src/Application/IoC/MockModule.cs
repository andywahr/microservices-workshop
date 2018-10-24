using Autofac;
using ContosoTravel.Web.Application.Data;
using ContosoTravel.Web.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoTravel.Web.Application.IoC
{
    public class MockModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {  
            builder.RegisterType<Data.Mock.FlightDataMockProvider>()
                   .As<IFlightDataProvider>()
                   .SingleInstance();

            builder.RegisterType<Data.Mock.CartDataMockProvider>()
                   .As<ICartDataProvider>()
                   .SingleInstance();

            builder.RegisterType<Data.Mock.AirportDataMockProvider>()
                   .As<IAirportDataProvider>()
                   .SingleInstance();
        }
    }
}
