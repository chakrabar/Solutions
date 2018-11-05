using System;
using System.Data;
using System.Data.SqlClient;

namespace AccessManagement.CommandStack.Contracts
{
    public interface ISqlHelper
    {
        string GetConnectionString();
        SqlConnection GetConnection();
        DataTable GetDataTable();
    }
}
