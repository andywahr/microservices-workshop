using ContosoTravel.Web.Application;
using ContosoTravel.Web.Application.Interfaces;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DataLoader.SQLServer
{
    public class SQLServerConnectionProvider : ISQLServerConnectionProvider
    {
        private readonly ContosoConfiguration _contosoConfiguration;

        public SQLServerConnectionProvider(ContosoConfiguration contosoConfiguration)
        {
            _contosoConfiguration = contosoConfiguration;
        }

        public async Task<SqlConnection> GetOpenConnection(CancellationToken cancellationToken)
        {
            var newConnection = new SqlConnection($"Server={_contosoConfiguration.DataAccountName}.database.windows.net;Database={_contosoConfiguration.DatabaseName};User Id={_contosoConfiguration.DataAdministratorLogin};Password={_contosoConfiguration.DataAdministratorLoginPassword};");
            await newConnection.OpenAsync(cancellationToken);
            return newConnection;
        }
    }
}
