using System;
using CarRental.iterator;
using CarRental.mediator;

namespace CarRental
{
    public partial class AdminSignUp : System.Web.UI.Page
    {
        private ISignUpMediator _signUpMediator;
        private UserAccountCollection _userAccounts;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || Session["Role"] == null || Session["Role"].ToString() != "admin")
            {
                Response.Redirect("Login.aspx");
            }

            _userAccounts = new UserAccountCollection();
            _signUpMediator = new SignUpMediator(_userAccounts);
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                string email = txtEmail.Text;
                string phone = txtPhone.Text;
                string role = ddlRole.SelectedValue;

                _signUpMediator.RegisterUser(username, password, email, phone, role);

                lblStatus.Text = "Account created successfully!";
            }
        }
    }
}
