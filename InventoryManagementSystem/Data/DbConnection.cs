using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace InventoryManagementSystem.Data
{
    public class DbConnection
    {
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }

        public DbConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
