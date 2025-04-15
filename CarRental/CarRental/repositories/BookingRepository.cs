using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CarRental.repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly string _connStr;

        public BookingRepository()
        {
            _connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;
        }

        public DataTable GetPendingReturns(int userId)
        {
            string sql = "SELECT b.BookingId, b.StartDate, b.EndDate, b.TotalCost, b.Deposit, c.Brand, c.Type, c.CarImage " +
                         "FROM Bookings b " +
                         "JOIN CarDetails c ON b.CarId = c.CarId " +
                         "WHERE b.UserId = @UserId AND b.IsReturned = 0";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
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

        public void UpdateBookingReturn(int bookingId, decimal damageCost, decimal finalTotalCost)
        {
            string sql = "UPDATE Bookings SET IsReturned = 1, DamageCost = @DamageCost, FinalTotalCost = @FinalTotalCost, FinalDeposit = Deposit WHERE BookingId = @BookingId";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@DamageCost", damageCost);
                    cmd.Parameters.AddWithValue("@FinalTotalCost", finalTotalCost);
                    cmd.Parameters.AddWithValue("@BookingId", bookingId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
