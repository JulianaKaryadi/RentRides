using System;
using System.Data.SqlClient;
using System.Configuration;

namespace CarRental.command
{
    public class BookingService
    {
        public void CreateBooking(Booking booking)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "INSERT INTO Bookings (UserId, CarId, StartDate, EndDate, TotalCost, Deposit, FinalTotalCost, FinalDeposit) " +
                             "VALUES (@UserId, @CarId, @StartDate, @EndDate, @TotalCost, @Deposit, NULL, NULL)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", booking.UserId);
                    cmd.Parameters.AddWithValue("@CarId", booking.CarId);
                    cmd.Parameters.AddWithValue("@StartDate", booking.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", booking.EndDate);
                    cmd.Parameters.AddWithValue("@TotalCost", booking.TotalCost);
                    cmd.Parameters.AddWithValue("@Deposit", booking.Deposit);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CancelBooking(int bookingId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "DELETE FROM Bookings WHERE BookingId = @BookingId";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@BookingId", bookingId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
