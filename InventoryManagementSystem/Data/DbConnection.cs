using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace InventoryManagementSystem.Data
{
    public class DbConnection
    {
        private readonly IConfiguration _config;

        public DbConnection(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection GetConnection()
        {
            var connStr = _config.GetConnectionString("DefaultConnection");
            return new SqlConnection(connStr);
        }
    }
}
