using Autofac;
using Autofac.Extensions.DependencyInjection;
using ContosoTravel.Web.Application.Data.Mock;
using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nito.AsyncEx;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataLoader
{
    public class Program
    {
        static IContainer Container = null;
        static AutofacServiceProvider ServiceProvider = null;
        static IConfiguration AppConfig = null;

        static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));
        }

        static async Task MainAsync(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            AppConfig = new ConfigurationBuilder()
                           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                           .AddEnvironmentVariables().Build();
            ContosoTravel.Web.Application.ContosoConfiguration.PopulateFromConfig((name) => AppConfig[name]);
            SetupIoC();
            await DataLoad(cts.Token);
        }

        private static async Task DataLoad(CancellationToken cancellationToken)
        {
            await LoadData<IAirportDataProvider, AirportModel>(cancellationToken);
            await LoadData<ICarDataProvider, CarModel>(cancellationToken);
            await LoadData<IHotelDataProvider, HotelModel>(cancellationToken);
            await LoadData<IFlightDataProvider, FlightModel>(cancellationToken);
        }

        private static async Task LoadData<IData, DataType>(CancellationToken cancellationToken)
        {
            var dataAdapter = (IWritableDataProvider<DataType>)Container.Resolve<IData>();
            var dataSource = Container.Resolve<IGetAllProvider<DataType>>();

            var allItems = await dataSource.GetAll(cancellationToken);
            Console.WriteLine($"Loading {typeof(DataType).Name}");

            foreach (DataType item in allItems)
            {
                await dataAdapter.Persist(item, cancellationToken);
            }
        }

        private static void SetupIoC()
        {
            // The Microsoft.Extensions.DependencyInjection.ServiceCollection
            // has extension methods provided by other .NET Core libraries to
            // regsiter services with DI.
            var serviceCollection = new ServiceCollection();

            // The Microsoft.Extensions.Logging package provides this one-liner
            // to add logging services.
            serviceCollection.AddLogging();

            var containerBuilder = new ContainerBuilder();

            // Once you've registered everything in the ServiceCollection, call
            // Populate to bring those registrations into Autofac. This is
            // just like a foreach over the list of things in the collection
            // to add them to Autofac.
            containerBuilder.Populate(serviceCollection);

            containerBuilder.RegisterAssemblyModules(typeof(Program).Assembly, typeof(ContosoTravel.Web.Application.ContosoConfiguration).Assembly);

            Container = containerBuilder.Build();
            ServiceProvider = new AutofacServiceProvider(Container);
        }
    }
}
