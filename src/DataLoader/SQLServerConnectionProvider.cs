using ContosoTravel.Web.Application.Interfaces;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DataLoader
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
