using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarRental.factories
{
    public class DeleteCommandFactory : ISqlCommandFactory
    {
        public SqlCommand CreateCommand(SqlConnection connection, params object[] parameters)
        {
            string sql = "DELETE FROM UserAccount WHERE UserId = @UserId";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@UserId", parameters[0]);
            return command;
        }
    }
}
