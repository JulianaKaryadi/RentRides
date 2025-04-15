using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using CarRental.command;

namespace CarRental
{
    public partial class Booking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadCars();
            }
        }

        private void LoadCars()
        {
            try
            {
                string sql = "SELECT CarId, Brand + ' ' + Type AS CarName FROM CarDetails";
                string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        lbCars.DataSource = reader;
                        lbCars.DataTextField = "CarName";
                        lbCars.DataValueField = "CarId";
                        lbCars.DataBind();
                    }
                }

                lbCars.Items.Insert(0, new ListItem("Select a car", ""));
            }
            catch (SqlException ex)
            {
                throw new CustomException("Failed to load cars from the database.", ex);
            }
            catch (Exception ex)
            {
                throw new CustomException("An unexpected error occurred while loading cars.", ex);
            }
        }

        protected void lbCars_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lbCars.SelectedValue != "")
                {
                    LoadCarDetails(int.Parse(lbCars.SelectedValue));
                }
                else
                {
                    carDetails.Visible = false;
                }
            }
            catch (CustomException ex)
            {
                SetStatus(ex.Message, System.Drawing.Color.Red);
            }
            catch (Exception)
            {
                SetStatus("An unexpected error occurred while selecting a car.", System.Drawing.Color.Red);
            }
        }

        private void LoadCarDetails(int carId)
        {
            try
            {
                string sql = "SELECT * FROM CarDetails WHERE CarId = @CarId";
                string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CarId", carId);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                imgCar.ImageUrl = "~/Images/" + reader["CarImage"].ToString();
                                lblCarName.Text = reader["Brand"].ToString() + " " + reader["Type"].ToString();
                                lblYear.Text = reader["Year"].ToString();
                                lblColor.Text = reader["Color"].ToString();
                                lblTransmission.Text = reader["Transmission"].ToString();
                                lblSeats.Text = reader["Seats"].ToString();
                                lblFuelType.Text = reader["FuelType"].ToString();
                                lblDailyRate.Text = decimal.Parse(reader["DailyRate"].ToString()).ToString("F2");

                                carDetails.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new CustomException("Failed to load car details from the database.", ex);
            }
            catch (Exception ex)
            {
                throw new CustomException("An unexpected error occurred while loading car details.", ex);
            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = DateTime.Parse(txtStartDate.Text);
                DateTime endDate = DateTime.Parse(txtEndDate.Text);
                if (endDate < startDate)
                {
                    throw new CustomException("End date cannot be before start date.");
                }

                int days = (endDate - startDate).Days + 1;
                decimal insurance = 50m; 

                if (lbCars.GetSelectedIndices().Length > 0)
                {
                    decimal totalBaseCost = 0;
                    foreach (ListItem item in lbCars.Items)
                    {
                        if (item.Selected)
                        {
                            int carId = int.Parse(item.Value);
                            string sql = "SELECT DailyRate FROM CarDetails WHERE CarId = @CarId";
                            string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;

                            using (SqlConnection conn = new SqlConnection(connStr))
                            {
                                using (SqlCommand cmd = new SqlCommand(sql, conn))
                                {
                                    cmd.Parameters.AddWithValue("@CarId", carId);
                                    conn.Open();
                                    decimal dailyRate = (decimal)cmd.ExecuteScalar();
                                    totalBaseCost += days * dailyRate;
                                }
                            }
                        }
                    }
                    decimal tax = totalBaseCost * 0.06m;
                    decimal totalCost = totalBaseCost + insurance + tax;
                    decimal deposit = totalCost * 0.4m;

                    lblDaysValue.Text = days.ToString();
                    lblInsuranceValue.Text = insurance.ToString("C");
                    lblTaxValue.Text = tax.ToString("C");
                    lblCostValue.Text = totalCost.ToString("C");
                    lblDepositValue.Text = deposit.ToString("C");

                    SetStatus("Total cost calculated. Please enter the deposit amount.", System.Drawing.Color.Green);
                }
            }
            catch (CustomException ex)
            {
                SetStatus(ex.Message, System.Drawing.Color.Red);
            }
            catch (SqlException ex)
            {
                throw new CustomException("Failed to calculate the cost from the database.", ex);
            }
            catch (Exception ex)
            {
                throw new CustomException("An unexpected error occurred while calculating the cost.", ex);
            }
        }

        protected void btnBook_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = DateTime.Parse(txtStartDate.Text);
                DateTime endDate = DateTime.Parse(txtEndDate.Text);
                if (endDate < startDate)
                {
                    throw new CustomException("End date cannot be before start date.");
                }

                int userId = (int)Session["UserId"];
                decimal totalCost = decimal.Parse(lblCostValue.Text, System.Globalization.NumberStyles.Currency);
                decimal deposit = decimal.Parse(txtDeposit.Text);

                if (deposit != totalCost * 0.4m)
                {
                    throw new CustomException("Deposit should be 40% of the total cost.");
                }

                var bookingService = new BookingService();
                var bookingManager = new BookingManager();

                foreach (ListItem item in lbCars.Items)
                {
                    if (item.Selected)
                    {
                        int carId = int.Parse(item.Value);
                        var booking = new Booking
                        {
                            UserId = userId,
                            CarId = carId,
                            StartDate = startDate,
                            EndDate = endDate,
                            TotalCost = totalCost,
                            Deposit = deposit
                        };

                        var createBookingCommand = new CreateBookingCommand(bookingService, booking);
                        bookingManager.AddCommand(createBookingCommand);
                    }
                }

                bookingManager.ExecuteCommands();

                lblStatus.Text = "Booking successful!";
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }
            catch (CustomException ex)
            {
                SetStatus(ex.Message, System.Drawing.Color.Red);
            }
            catch (SqlException ex)
            {
                throw new CustomException("Failed to save the booking to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new CustomException("An unexpected error occurred while saving the booking.", ex);
            }
        }

        private void SetStatus(string message, System.Drawing.Color color)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = color;
        }
    }
}
