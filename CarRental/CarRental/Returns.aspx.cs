using System;
using System.Data;
using System.Web.UI.WebControls;
using CarRental.repositories;

namespace CarRental
{
    public partial class Returns : System.Web.UI.Page
    {
        private readonly IBookingRepository _bookingRepository;

        public Returns()
        {
            _bookingRepository = new BookingRepository();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadReturns();
            }
        }

        private void LoadReturns()
        {
            int userId = (int)Session["UserId"];
            DataTable dt = _bookingRepository.GetPendingReturns(userId);
            rptReturns.DataSource = dt;
            rptReturns.DataBind();
        }

        protected void ddlDamage_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlDamage = (DropDownList)sender;
            RepeaterItem item = (RepeaterItem)ddlDamage.NamingContainer;

            Label lblNewTotalValue = (Label)item.FindControl("lblNewTotalValue");
            Label lblTotalCost = (Label)item.FindControl("lblTotalCost");
            Label lblDeposit = (Label)item.FindControl("lblDeposit");
            TextBox txtAmountPaid = (TextBox)item.FindControl("txtAmountPaid");

            decimal totalCost = decimal.Parse(lblTotalCost.Text.Replace("$", ""));
            decimal damageCost = 0;
            decimal deposit = decimal.Parse(lblDeposit.Text.Replace("$", ""));
            if (!decimal.TryParse(ddlDamage.SelectedValue, out damageCost))
            {
                damageCost = 0;
            }
            decimal amountPaid = string.IsNullOrEmpty(txtAmountPaid.Text) ? 0 : decimal.Parse(txtAmountPaid.Text);

            decimal newTotalCost = totalCost + damageCost - deposit - amountPaid;
            lblNewTotalValue.Text = newTotalCost.ToString("F2");
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int bookingId = int.Parse(btn.CommandArgument);

            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            DropDownList ddlDamage = (DropDownList)item.FindControl("ddlDamage");
            TextBox txtAmountPaid = (TextBox)item.FindControl("txtAmountPaid");
            Label lblNewTotalValue = (Label)item.FindControl("lblNewTotalValue");

            decimal damageCost = 0;
            if (!decimal.TryParse(ddlDamage.SelectedValue, out damageCost))
            {
                damageCost = 0; 
            }
            decimal amountPaid = decimal.Parse(txtAmountPaid.Text);
            decimal newTotalCost;

            if (decimal.TryParse(lblNewTotalValue.Text.Replace("RM", ""), out newTotalCost))
            {
                _bookingRepository.UpdateBookingReturn(bookingId, damageCost, newTotalCost);
                LoadReturns();
            }
            else
            {
                lblNewTotalValue.Text = "Error calculating total cost";
                lblNewTotalValue.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
