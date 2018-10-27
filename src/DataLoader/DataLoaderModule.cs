using Autofac;
using ContosoTravel.Web.Application;
using ContosoTravel.Web.Application.Data.Mock;
using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLoader
{
    public class DataLoaderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AirportDataMockProvider>()
                   .As<IGetAllProvider<AirportModel>>()
                   .SingleInstance();

            builder.RegisterType<FlightDataMockProvider>()
                   .As<IGetAllProvider<FlightModel>>()
                   .SingleInstance();

            builder.RegisterType<CarDataMockProvider>()
                   .As<IGetAllProvider<CarModel>>()
                   .SingleInstance();

            builder.RegisterType<HotelDataMockProvider>()
                   .As<IGetAllProvider<HotelModel>>()
                   .SingleInstance();
        }
    }
}
