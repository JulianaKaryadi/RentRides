using System;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI;

namespace CarRental
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();

                if (Page.IsValid)
                {
                    string username = txtUsername.Text;
                    string password = txtPassword.Text;
                    string email = txtEmail.Text;
                    string phone = txtPhone.Text;

                    string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;

                    if (IsEmailExists(email))
                    {
                        throw new CustomException("The email address is already registered.");
                    }

                    string sql = "INSERT INTO UserAccount (Username, PasswordHash, Email, Phone, Role, Enabled) VALUES (@Username, @PasswordHash, @Email, @Phone, @Role, @Enabled)";

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@Username", username);
                            cmd.Parameters.AddWithValue("@PasswordHash", HashPassword(password));
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@Phone", phone);
                            cmd.Parameters.AddWithValue("@Role", "user"); 
                            cmd.Parameters.AddWithValue("@Enabled", true); 

                            conn.Open();
                            int result = cmd.ExecuteNonQuery();

                            if (result > 0)
                            {
                                SetStatus("Registration successful!", System.Drawing.Color.Green);
                            }
                            else
                            {
                                throw new CustomException("Registration failed. Please try again.");
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
                SetStatus($"A database error occurred: {ex.Message}", System.Drawing.Color.Red);
                LogException(ex);
            }
            catch (Exception ex)
            {
                SetStatus("An unexpected error occurred. Please try again later.", System.Drawing.Color.Red);
                LogException(ex);
            }
        }

        private bool IsEmailExists(string email)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;
            string sql = "SELECT COUNT(*) FROM UserAccount WHERE Email = @Email";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        private void SetStatus(string message, System.Drawing.Color color)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = color;
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
