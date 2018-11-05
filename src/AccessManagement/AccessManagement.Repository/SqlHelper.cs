using System.Configuration;
using System.Data.SqlClient;

namespace AccessManagement.Repository
{
    public class SqlHelper //TODO: remove deplucation from DataAccess
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["UserAccess"].ConnectionString;
        }

        public static SqlConnection GetConnection()
        {
            var connection = new SqlConnection(GetConnectionString());
            connection.Open();

            return connection;
        }
    }
}
