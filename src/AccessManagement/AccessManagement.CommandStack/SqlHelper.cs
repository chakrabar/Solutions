using AccessManagement.CommandStack.Contracts;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AccessManagement.CommandStack
{
    public class SqlHelper : ISqlHelper
    {
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["UserAccess"].ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            var connection = new SqlConnection(GetConnectionString());
            connection.Open();

            return connection;
        }

        public DataTable GetDataTable()
        {
            return new DataTable();
        }
    }
}
