using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using CarRental.interpreter;

namespace CarRental
{
    public partial class ServiceCatalogue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBrands();
                BindCarDetails();
            }
        }

        private void BindBrands()
        {
            string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;
            string sql = "SELECT DISTINCT Brand FROM CarDetails";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlBrand.DataSource = reader;
                    ddlBrand.DataTextField = "Brand";
                    ddlBrand.DataValueField = "Brand";
                    ddlBrand.DataBind();
                }
            }

            ddlBrand.Items.Insert(0, new ListItem("All", ""));
        }

        private void BindCarDetails(string brand = "", int? seats = null, decimal? minRate = null, decimal? maxRate = null)
        {
            string sql = "SELECT * FROM CarDetails WHERE 1=1";
            string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    var context = new CarFilterContext();
                    context.AddExpression(new BrandExpression(brand));
                    context.AddExpression(new SeatsExpression(seats));
                    context.AddExpression(new RateExpression(minRate, maxRate));
                    context.Interpret(cmd);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        rptCarDetails.DataSource = dt;
                        rptCarDetails.DataBind();
                    }
                }
            }
        }

        protected void FilterCars(object sender, EventArgs e)
        {
            string brand = ddlBrand.SelectedValue;
            int? seats = string.IsNullOrEmpty(ddlSeats.SelectedValue) ? (int?)null : int.Parse(ddlSeats.SelectedValue);
            decimal? minRate = string.IsNullOrEmpty(txtMinRate.Text) ? (decimal?)null : decimal.Parse(txtMinRate.Text);
            decimal? maxRate = string.IsNullOrEmpty(txtMaxRate.Text) ? (decimal?)null : decimal.Parse(txtMaxRate.Text);

            BindCarDetails(brand, seats, minRate, maxRate);
        }
    }
}
