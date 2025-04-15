using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI;

namespace CarRental
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate(); 
                if (!Page.IsValid) 
                {
                    throw new CustomException("All fields are required.");
                }

                string sql = @"SELECT * FROM UserAccount WHERE Username = @username";
                string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedPasswordHash = reader["PasswordHash"].ToString();
                                string role = reader["Role"].ToString();
                                bool enabled = Convert.ToBoolean(reader["Enabled"]);
                                int userId = Convert.ToInt32(reader["UserId"]);
                                string password = txtPassword.Text;

                                string inputPasswordHash = HashPassword(password);

                                if (inputPasswordHash == storedPasswordHash && enabled)
                                {
                                    Session["UserName"] = txtUsername.Text;
                                    Session["Role"] = role;
                                    Session["UserId"] = userId;

                                    if (role == "admin")
                                    {
                                        Response.Redirect("Users.aspx");
                                    }
                                    else
                                    {
                                        Response.Redirect("ServiceCatalogue.aspx");
                                    }
                                }
                                else
                                {
                                    throw new CustomException("Incorrect password or account disabled.");
                                }
                            }
                            else
                            {
                                throw new CustomException("Incorrect username.");
                            }
                        }
                    }
                }
            }
            catch (CustomException ex)
            {
                SetStatus(ex.Message, System.Drawing.Color.Red);
                LogException(ex);
            }
            catch (SqlException ex)
            {
                SetStatus("A database error occurred. Please try again later.", System.Drawing.Color.Red);
                LogException(ex);
            }
        }

        private string HashPassword(string password)
        {
            try
            {
                using (var sha256 = System.Security.Cryptography.SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    System.Text.StringBuilder builder = new System.Text.StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new CustomException("An error occurred while hashing the password.", ex);
            }
        }

        private void SetStatus(string message, System.Drawing.Color color)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = color;
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
    }
}
