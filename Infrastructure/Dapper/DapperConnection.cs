using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Dapper
{
    public class DapperConnection : IDapperConnection, IDisposable
    {
        private readonly IConfiguration Configuration;
        private IDbConnection _dbConnection { get; set; }

        public DapperConnection(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IDbConnection GetConnection()
        {
            if (_dbConnection == null || _dbConnection.State == ConnectionState.Closed)
            {
                return GetSQLServerConnection();
            }

            return _dbConnection;
        }

        public void Dispose()
        {
            _dbConnection?.Close();
            _dbConnection?.Dispose();
        }

        private IDbConnection GetSQLServerConnection()
        {
            _dbConnection = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            _dbConnection.Open();
            return _dbConnection;
        }
    }
}
