using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarRental.factories
{
    public class SelectCommandFactory : ISqlCommandFactory
    {
        public SqlCommand CreateCommand(SqlConnection connection, params object[] parameters)
        {
            string sql = "SELECT UserId, Username, Email, Phone, Role, Enabled FROM UserAccount";
            SqlCommand command = new SqlCommand(sql, connection);
            return command;
        }
    }
}
