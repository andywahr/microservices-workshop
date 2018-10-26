using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoTravel.Web.Application
{
    public static class Configuration
    {
        public static ServicesType ServicesType { get; set; } = ServicesType.Monolith;
        public static DataType DataType { get; set; } = DataType.Mock;
        public static string ServicesMiddlewareAccountName { get; set; }
        public static string DataAccountName { get; set; }
        public static string SubscriptionId { get; set; }
        public static string ResourceGroupName { get; set; }
        public static string AzureRegion { get; set; }

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
