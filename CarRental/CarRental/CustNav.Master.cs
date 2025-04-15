using System;
using System.Web;

namespace CarRental
{
    public partial class CustNav : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateNavigation();
            }
        }

        private void UpdateNavigation()
        {
            if (Session["UserId"] == null)
            {
                // User is not logged in
                linkLogin.Visible = true;
                linkSignup.Visible = true;
                linkServiceCatalogue.Visible = false;
                linkBooking.Visible = false;
                linkReturns.Visible = false;
                linkHistory.Visible = false;
                linkLogout.Visible = false;

                // Admin links
                linkUsers.Visible = false;
                linkAddCar.Visible = false;
                linkAdminSignUp.Visible = false;
                linkCustomerFeedback.Visible = false;
            }
            else
            {
                // User is logged in
                linkLogin.Visible = false;
                linkSignup.Visible = false;
                linkLogout.Visible = true;

                if (Session["Role"] != null && Session["Role"].ToString() == "admin")
                {
                    // Admin navigation
                    linkServiceCatalogue.Visible = false;
                    linkBooking.Visible = false;
                    linkReturns.Visible = false;
                    linkHistory.Visible = false;

                    linkUsers.Visible = true;
                    linkAddCar.Visible = true;
                    linkAdminSignUp.Visible = true;
                    linkCustomerFeedback.Visible = true;
                }
                else
                {
                    // User navigation
                    linkServiceCatalogue.Visible = true;
                    linkBooking.Visible = true;
                    linkReturns.Visible = true;
                    linkHistory.Visible = true;

                    linkUsers.Visible = false;
                    linkAddCar.Visible = false;
                    linkAdminSignUp.Visible = false;
                    linkCustomerFeedback.Visible = false;
                }
            }
        }
    }
}
