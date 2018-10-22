using ContosoTravel.Web.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoTravel.Web.Application.Data
{
    public class SQLServerDataProvider : IDataProvider
    {
        public string GetProvider()
        {
            return "SQL Server";
        }
    }
}
