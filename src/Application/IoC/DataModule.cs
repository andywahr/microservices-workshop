using Autofac;
using ContosoTravel.Web.Application.Data;
using ContosoTravel.Web.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoTravel.Web.Application.IoC
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CartDisplayProvider>().AsSelf().SingleInstance();

            switch (Configuration.DataType)
            {
                case DataType.Mock:
                    builder.RegisterType<Data.Mock.AirportDataMockProvider>()
                           .As<ICarDataProvider>()
                           .SingleInstance();

                    builder.RegisterType<Data.Mock.FlightDataMockProvider>()
                           .As<IFlightDataProvider>()
                           .SingleInstance();

                    builder.RegisterType<Data.Mock.CarDataMockProvider>()
                           .As<ICarDataProvider>()
                           .SingleInstance();

                    builder.RegisterType<Data.Mock.HotelDataMockProvider>()
                           .As<IHotelDataProvider>()
                           .SingleInstance();

                    builder.RegisterType<Data.Mock.CartDataMockProvider>()
                           .As<ICartDataProvider>()
                           .SingleInstance();

                    builder.RegisterType<Data.Mock.ItineraryDataMockProvider>()
                           .As<IItineraryDataProvider>()
                           .SingleInstance();
                    break;
            }
        }
    }
}
