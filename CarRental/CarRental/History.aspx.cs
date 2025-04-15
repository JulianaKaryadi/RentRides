using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using CarRental.observer;

namespace CarRental
{
    public partial class History : System.Web.UI.Page
    {
        private readonly ISubject bookingSubject;

        public History()
        {
            bookingSubject = new BookingSubject();
            bookingSubject.Attach(new BookingObserver());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadHistory();
            }
        }

        private void LoadHistory()
        {
            int userId = (int)Session["UserId"];
            string sql = "SELECT b.BookingId, b.StartDate, b.EndDate, b.TotalCost, b.Deposit, b.DamageCost, b.Rating, b.Comment, b.FinalTotalCost, b.FinalDeposit, c.Brand, c.Type, c.CarImage FROM Bookings b JOIN CarDetails c ON b.CarId = c.CarId WHERE b.UserId = @UserId AND b.IsReturned = 1";
            string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        rptHistory.DataSource = dt;
                        rptHistory.DataBind();
                    }
                }
            }
        }

        protected void btnRate_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int bookingId = int.Parse(btn.CommandArgument);

            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            RadioButtonList rblRating = (RadioButtonList)item.FindControl("rblRating");
            TextBox txtComment = (TextBox)item.FindControl("txtComment");
            Label lblRatingDisplay = (Label)item.FindControl("lblRatingDisplay");
            Label lblCommentDisplay = (Label)item.FindControl("lblCommentDisplay");
            Label lblAppreciation = (Label)item.FindControl("lblAppreciation");

            if (rblRating.SelectedItem != null)
            {
                int rating = int.Parse(rblRating.SelectedValue);
                string comment = txtComment.Text;

                string sql = "UPDATE Bookings SET Rating = @Rating, Comment = @Comment WHERE BookingId = @BookingId";
                string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Rating", rating);
                        cmd.Parameters.AddWithValue("@Comment", comment);
                        cmd.Parameters.AddWithValue("@BookingId", bookingId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                lblRatingDisplay.Text = $"Your Rating: {rating} stars";
                lblCommentDisplay.Text = $"Your Comment: {comment}";
                lblAppreciation.Text = "Thank you for your feedback!";
                lblAppreciation.Visible = true;

                bookingSubject.Notify(bookingId, rating, comment);
            }
        }
    }
}
