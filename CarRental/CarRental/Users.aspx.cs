using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarRental.factories;

namespace CarRental
{
    public partial class Users : System.Web.UI.Page
    {
        private ISqlCommandFactory selectFactory;
        private ISqlCommandFactory updateFactory;
        private ISqlCommandFactory deleteFactory;

        public Users()
        {
            selectFactory = new SelectCommandFactory();
            updateFactory = new UpdateCommandFactory();
            deleteFactory = new DeleteCommandFactory();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = selectFactory.CreateCommand(conn);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvUsers.DataSource = dt;
                    gvUsers.DataBind();
                }
            }
        }

        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;
            BindGrid();
        }

        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int userId = (int)gvUsers.DataKeys[e.RowIndex].Value;
            GridViewRow row = gvUsers.Rows[e.RowIndex];

            string username = ((TextBox)row.Cells[1].FindControl("txtUsername")).Text;
            string email = ((TextBox)row.Cells[2].FindControl("txtEmail")).Text;
            string phone = ((TextBox)row.Cells[3].FindControl("txtPhone")).Text;
            string role = ((TextBox)row.Cells[4].FindControl("txtRole")).Text;
            bool enabled = ((CheckBox)row.Cells[5].FindControl("chkEnabled")).Checked;

            string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = updateFactory.CreateCommand(conn, username, email, phone, role, enabled, userId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            gvUsers.EditIndex = -1;
            BindGrid();
        }

        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userId = (int)gvUsers.DataKeys[e.RowIndex].Value;
            string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = deleteFactory.CreateCommand(conn, userId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            BindGrid();
        }
    }
}
