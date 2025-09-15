using Microsoft.Data.SqlClient;

namespace Demo_ASP_FirstTry.App.Data
{
    public class SQLConnectionFactory
    {
        private readonly string _connectionString;

        public SQLConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        } 
    }
}
