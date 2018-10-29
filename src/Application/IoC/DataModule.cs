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

            switch (ContosoConfiguration.DataType)
            {
                case DataType.Mock:
                    builder.RegisterType<Data.Mock.AirportDataMockProvider>()
                           .As<IAirportDataProvider>()
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

                case DataType.CosmosSQL:
                    builder.RegisterType<Data.CosmosSQL.CosmosDBProvider>()
                           .AsSelf()
                           .SingleInstance();

                    builder.RegisterType<Data.CosmosSQL.AirportDataCosmosSQLProvider>()
                           .As<IAirportDataProvider>()
                           .SingleInstance();

                    builder.RegisterType<Data.CosmosSQL.FlightDataCosmosSQLProvider>()
                           .As<IFlightDataProvider>()
                           .SingleInstance();

                    builder.RegisterType<Data.CosmosSQL.CarDataCosmosSQLProvider>()
                           .As<ICarDataProvider>()
                           .SingleInstance();

                    builder.RegisterType<Data.CosmosSQL.HotelDataCosmosSQLProvider>()
                           .As<IHotelDataProvider>()
                           .SingleInstance();

                    builder.RegisterType<Data.CosmosSQL.CartDataCosmosSQLProvider>()
                           .As<ICartDataProvider>()
                           .SingleInstance();

                    builder.RegisterType<Data.CosmosSQL.ItineraryDataCosmosSQLProvider>()
                           .As<IItineraryDataProvider>()
                           .SingleInstance();
                    break;
            }
        }
    }
}
