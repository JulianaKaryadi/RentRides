using System;
using System.Data.SqlClient;
using System.Configuration;
using CarRental.iterator;
using CarRental.state;

namespace CarRental.mediator
{
    public class SignUpMediator : ISignUpMediator
    {
        private UserAccountCollection _userAccounts;

        public SignUpMediator(UserAccountCollection userAccounts)
        {
            _userAccounts = userAccounts;
        }

        public void RegisterUser(string username, string password, string email, string phone, string role)
        {
            var userAccount = new UserAccount
            {
                Username = username,
                PasswordHash = HashPassword(password),
                Email = email,
                Phone = phone,
                Role = role,
                Enabled = true
            };

            _userAccounts.AddUserAccount(userAccount);

            string connStr = ConfigurationManager.ConnectionStrings["connRental"].ConnectionString;
            string sql = "INSERT INTO UserAccount (Username, PasswordHash, Email, Phone, Role, Enabled) VALUES (@Username, @PasswordHash, @Email, @Phone, @Role, @Enabled)";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", userAccount.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", userAccount.PasswordHash);
                    cmd.Parameters.AddWithValue("@Email", userAccount.Email);
                    cmd.Parameters.AddWithValue("@Phone", userAccount.Phone);
                    cmd.Parameters.AddWithValue("@Role", userAccount.Role);
                    cmd.Parameters.AddWithValue("@Enabled", userAccount.Enabled);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var builder = new System.Text.StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
