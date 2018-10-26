using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace ContosoTravel.Web.Application
{
    public static class Configuration
    {
        public static void PopulateFromConfig(Func<string, string> getVal)
        {
            Application.Configuration.DataType = (DataType)Enum.Parse(typeof(DataType), getVal("DataType"));
            Application.Configuration.ServicesType = (ServicesType)Enum.Parse(typeof(ServicesType), getVal("ServicesType"));
            Application.Configuration.ServicesMiddlewareAccountName = getVal("ServicesMiddlewareAccountName");
            Application.Configuration.DataAccountName = getVal("DataAccountName");
            Application.Configuration.SubscriptionId = getVal("SubscriptionId");
            Application.Configuration.ResourceGroupName = getVal("ResourceGroupName");
            Application.Configuration.AzureRegion = getVal("AzureRegion");
            Application.Configuration.TenantId = getVal("TenantId");
        }

        public static ServicesType ServicesType { get; set; } = ServicesType.Monolith;
        public static DataType DataType { get; set; } = DataType.Mock;
        public static string ServicesMiddlewareAccountName { get; set; }
        public static string DataAccountName { get; set; }
        public static string SubscriptionId { get; set; }
        public static string ResourceGroupName { get; set; }
        public static string AzureRegion { get; set; }
        public static string TenantId { get; set; }
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
        Cosmos,
        SQL
    }
}
