using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CarRental.facade
{
    public class DatabaseFacade
    {
        private readonly string _connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;

        public DataTable ExecuteQuery(string sql)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
        }
    }
}
