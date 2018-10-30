using ContosoTravel.Web.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Functions.ServiceBus
{
    public class SQLServerConnectionProvider : ISQLServerConnectionProvider
    {
        public async Task<SqlConnection> GetOpenConnection(CancellationToken cancellationToken)
        {
            var newConnection = new SqlConnection("Server=andywahr-contosotravel-sqldb.database.windows.net;Database=ContosoTravel;User Id=contosoAdmin;Password=Password#1;");
            await newConnection.OpenAsync(cancellationToken);
            return newConnection;
        }
    }
}
