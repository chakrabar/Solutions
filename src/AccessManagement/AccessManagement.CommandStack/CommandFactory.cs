using System.Data;
using System.Data.SqlClient;

namespace AccessManagement.CommandStack
{
    public class CommandFactory
    {
        public static SqlCommand CreateSPCommand(DataTable dt, SqlConnection conn, string spName, string paramName)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = spName;
            SqlParameter param = cmd.Parameters.AddWithValue(paramName, dt);
            return cmd;
        }
    }
}
