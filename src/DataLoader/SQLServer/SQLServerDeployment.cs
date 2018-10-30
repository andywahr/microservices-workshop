using ContosoTravel.Web.Application;
using ContosoTravel.Web.Application.Interfaces;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataLoader.SQLServer
{
    public class SQLServerDeployment : IDataDeployment
    {
        private readonly ISQLServerConnectionProvider _connectionProvider;
        private readonly ContosoConfiguration _contosoConfiguration;

        public SQLServerDeployment(ISQLServerConnectionProvider connectionProvider, ContosoConfiguration contosoConfiguration)
        {
            _connectionProvider = connectionProvider;
            _contosoConfiguration = contosoConfiguration;
        }

        public async Task Configure(CancellationToken cancellationToken)
        {
            using (SqlConnection connection = new SqlConnection($"Server={_contosoConfiguration.DataAccountName}.database.windows.net;Database={_contosoConfiguration.DatabaseName};User Id={_contosoConfiguration.DataAdministratorLogin};Password={_contosoConfiguration.DataAdministratorLoginPassword};"))
            {
                await connection.OpenAsync(cancellationToken);
                Server server = new Server(new ServerConnection(connection));

                server.ConnectionContext.ExecuteNonQuery(ReadResource("Airports.sql"));
                server.ConnectionContext.ExecuteNonQuery(ReadResource("Cars.sql"));
                server.ConnectionContext.ExecuteNonQuery(ReadResource("Flights.sql"));
                server.ConnectionContext.ExecuteNonQuery(ReadResource("Hotels.sql"));
                server.ConnectionContext.ExecuteNonQuery(ReadResource("Carts.sql"));
                server.ConnectionContext.ExecuteNonQuery(ReadResource("Itineraries.sql"));
            }
        }

        private string ReadResource(string name)
        {
            var assembly = typeof(SQLServerDeployment).Assembly;
            using (StreamReader resource = new StreamReader(assembly.GetManifestResourceStream($"DataLoader.SQLServer.{name}")))
            {
                return resource.ReadToEnd();
            }
        }
    }    
}
