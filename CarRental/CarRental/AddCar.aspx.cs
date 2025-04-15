using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using CarRental.builder;
using CarRental.template;
using CarRental.chain;
using CarRental.composite;
using CarRental.flyweight;


namespace CarRental
{
    public partial class AddCar : System.Web.UI.Page
    {
        private CarComposite carComposite = new CarComposite();
        private CarFlyweightFactory carFlyweightFactory = new CarFlyweightFactory();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void btnAddCar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    string brand = txtBrand.Text;
                    string type = txtType.Text;
                    int year = int.Parse(txtYear.Text);
                    string color = txtColor.Text;
                    string transmission = txtTransmission.Text;
                    int seats = int.Parse(txtSeats.Text);
                    string fuelType = txtFuelType.Text;
                    decimal dailyRate = decimal.Parse(txtDailyRate.Text);
                    string noPlat = txtNoPlat.Text;

                    string carImage = Path.GetFileName(fuCarImage.PostedFile.FileName);
                    string imagePath = Server.MapPath("~/Images/") + carImage;
                    fuCarImage.SaveAs(imagePath);

                    carComposite.Add(new CarComponent(brand));
                    carComposite.Display();

                    var carFlyweight = carFlyweightFactory.GetFlyweight(brand, type);
                    carFlyweight.Display(color, transmission, seats, fuelType, dailyRate, noPlat, carImage);

                    var carAdder = new ConcreteCarAdder();
                    carAdder.AddCar(brand, type, year, color, transmission, seats, fuelType, dailyRate, noPlat, carImage);

                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    lblStatus.Text = "Car added successfully!";
                    BindGrid();
                }
                catch (CustomException ex)
                {
                    LogException(ex);
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = ex.Message;
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = "An unexpected error occurred. Please try again later.";
                }
            }
        }

        private void LogException(Exception ex)
        {
            try
            {
                string logFilePath = Server.MapPath("~/App_Data/ErrorLog.txt");
                string errorMessage = $"[{DateTime.Now}] Exception: {ex.Message}\nStack Trace: {ex.StackTrace}\n";

                if (ex.InnerException != null)
                {
                    errorMessage += $"Inner Exception: {ex.InnerException.Message}\nInner Stack Trace: {ex.InnerException.StackTrace}\n";
                }

                File.AppendAllText(logFilePath, errorMessage);
            }
            catch (Exception logEx)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to log exception: {logEx.Message}");
            }
        }

        protected void gvCarDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCarDetails.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        private void BindGrid()
        {
            string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;
            string sql = "SELECT Brand, Type, Year, Color, Transmission, Seats, FuelType, DailyRate, NoPlat, CarImage FROM CarDetails";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvCarDetails.DataSource = dt;
                        gvCarDetails.DataBind();
                    }
                }
            }
        }

        protected void gvCarDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteCar")
            {
                string noPlat = e.CommandArgument.ToString(); 

                DeleteCar(noPlat);
                BindGrid();
            }
        }

        private void DeleteCar(string noPlat)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;
            string sql = "DELETE FROM CarDetails WHERE NoPlat = @NoPlat";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NoPlat", noPlat);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public class ConcreteCarAdder : CarAdderTemplate
    {
        protected override bool ValidateCarDetails(string brand, int year, decimal dailyRate)
        {
            var brandValidator = new CarBrandValidator();
            var yearValidator = new CarYearValidator();
            var rateValidator = new CarRateValidator();

            brandValidator.SetNext(yearValidator);
            yearValidator.SetNext(rateValidator);

            return brandValidator.Validate(brand, year, dailyRate);
        }

        protected override SqlCommand BuildCarInsertCommand(string brand, string type, int year, string color, string transmission, int seats, string fuelType, decimal dailyRate, string noPlat, string carImage)
        {
            var queryBuilder = new CarInsertQueryBuilder()
                .WithBrand(brand)
                .WithType(type)
                .WithYear(year)
                .WithColor(color)
                .WithTransmission(transmission)
                .WithSeats(seats)
                .WithFuelType(fuelType)
                .WithDailyRate(dailyRate)
                .WithNoPlat(noPlat)
                .WithCarImage(carImage);

            return queryBuilder.Build();
        }

        protected override void ExecuteCarInsertCommand(SqlCommand command)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                command.Connection = conn;
                conn.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new CustomException("Failed to add the car. Please check the details and try again.", ex);
                }
            }
        }
    }
}
