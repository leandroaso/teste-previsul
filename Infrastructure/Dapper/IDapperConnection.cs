using System.Data;

namespace Infrastructure.Dapper
{
    public interface IDapperConnection
    {
        IDbConnection GetConnection();
    }
}
