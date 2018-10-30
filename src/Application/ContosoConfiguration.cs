using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Extensions.Configuration;

namespace ContosoTravel.Web.Application
{
    public class ContosoConfiguration
    {
        public static ContosoConfiguration PopulateFromConfig(Func<string, string> getVal)
        {
            DataType = (DataType)Enum.Parse(typeof(DataType), getVal("DataType"));
            ServicesType = (ServicesType)Enum.Parse(typeof(ServicesType), getVal("ServicesType"));

            return new ContosoConfiguration()
                       {
                            ServicesMiddlewareAccountName = getVal("ServicesMiddlewareAccountName"),
                            DataAccountName = getVal("DataAccountName"),
                            DatabaseName = getVal("DatabaseName"),
                            SubscriptionId = getVal("SubscriptionId"),
                            ResourceGroupName = getVal("ResourceGroupName"),
                            AzureRegion = getVal("AzureRegion"),
                            TenantId = getVal("TenantId")
                       };
        }

        public static ContosoConfiguration PopulateFromConfig(IConfiguration appConfig)
        {
            IConfigurationSection contosoConfigSection = appConfig.GetSection("ContosoTravel");
            DataType = (DataType)Enum.Parse(typeof(DataType), contosoConfigSection["DataType"]);
            ServicesType = (ServicesType)Enum.Parse(typeof(ServicesType), contosoConfigSection["ServicesType"]);

            var newConfig = contosoConfigSection.Get<ContosoConfiguration>();
            return newConfig;
        }

        public static ServicesType ServicesType { get; set; } = ServicesType.Monolith;
        public static DataType DataType { get; set; } = DataType.Mock;
        public string ServicesMiddlewareAccountName { get; set; }
        public string DataAccountName { get; set; }
        public string DatabaseName { get; set; }
        public string SubscriptionId { get; set; }
        public string ResourceGroupName { get; set; }
        public string AzureRegion { get; set; }
        public string TenantId { get; set; }
    }

    public enum ServicesType
    {
        Monolith,
        ServiceBus,
        EventGrid
    }

    public enum DataType
    {
        Mock,
        CosmosSQL,
        SQL
    }
}
