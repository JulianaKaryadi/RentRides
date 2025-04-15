using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.factories
{
    public interface ISqlCommandFactory
    {
        SqlCommand CreateCommand(SqlConnection connection, params object[] parameters);
    }
}

