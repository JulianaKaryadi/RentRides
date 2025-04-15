using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarRental.factories
{
    public class UpdateCommandFactory : ISqlCommandFactory
    {
        public SqlCommand CreateCommand(SqlConnection connection, params object[] parameters)
        {
            string sql = "UPDATE UserAccount SET Username = @Username, Email = @Email, Phone = @Phone, Role = @Role, Enabled = @Enabled WHERE UserId = @UserId";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Username", parameters[0]);
            command.Parameters.AddWithValue("@Email", parameters[1]);
            command.Parameters.AddWithValue("@Phone", parameters[2]);
            command.Parameters.AddWithValue("@Role", parameters[3]);
            command.Parameters.AddWithValue("@Enabled", parameters[4]);
            command.Parameters.AddWithValue("@UserId", parameters[5]);
            return command;
        }
    }
}
