using System;
using System.Data;
using System.Web.UI.WebControls;
using CarRental.strategies;
using CarRental.decorator;
using CarRental.adapter;
using CarRental.facade;
using CarRental.proxy;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;

namespace CarRental
{
    public partial class CustomerFeedback : System.Web.UI.Page
    {
        private IReportStrategy reportStrategy;
        private DatabaseFacade dbFacade;

        protected void Page_Load(object sender, EventArgs e)
        {
            dbFacade = new DatabaseFacade();
            if (!IsPostBack)
            {
                LoadMonthlyBookingsStats();
                LoadIncomeStats();
                LoadFeedbackGrid();
                LoadTopUserAndRating();
                LoadContactUsers();
                LoadFeedbackAnalysis();
            }
        }

        protected void btnGenerateCSV_Click(object sender, EventArgs e)
        {
            var reportAccess = new ReportAccessProxy("Admin");
            if (reportAccess.CanGenerateReport("Admin"))
            {
                GenerateReport(new CsvReportStrategy(), "CustomerFeedback.csv");
            }
            else
            {
                lblStatus.Text = "You do not have permission to generate reports.";
            }
        }

        protected void btnGenerateJSON_Click(object sender, EventArgs e)
        {
            var reportAccess = new ReportAccessProxy("Admin");
            if (reportAccess.CanGenerateReport("Admin"))
            {
                GenerateReport(new JsonReportStrategy(), "CustomerFeedback.json");
            }
            else
            {
                lblStatus.Text = "You do not have permission to generate reports.";
            }
        }

        protected void btnGenerateXML_Click(object sender, EventArgs e)
        {
            var reportAccess = new ReportAccessProxy("Admin"); 
            if (reportAccess.CanGenerateReport("Admin"))
            {
                GenerateReport(new XmlReportStrategy(), "CustomerFeedback.xml");
            }
            else
            {
                lblStatus.Text = "You do not have permission to generate reports.";
            }
        }

        private void GenerateReport(IReportStrategy strategy, string fileName)
        {
            string sql = "SELECT * FROM Bookings";
            DataTable dt = dbFacade.ExecuteQuery(sql);

            this.reportStrategy = new TimestampDecorator(strategy);
            string reportContent = this.reportStrategy.GenerateReport(dt);
            string directoryPath = Server.MapPath("~/Reports");
            string filePath = Path.Combine(directoryPath, fileName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            System.IO.File.WriteAllText(filePath, reportContent);

            lblStatus.Text = $"{fileName} report generated successfully.";
        }

        private void LoadMonthlyBookingsStats()
        {
            string sql = @"
                SELECT DATEPART(YEAR, StartDate) AS Year, DATEPART(MONTH, StartDate) AS Month, COUNT(*) AS BookingCount
                FROM Bookings
                GROUP BY DATEPART(YEAR, StartDate), DATEPART(MONTH, StartDate)
                ORDER BY Year, Month";

            DataTable dt = dbFacade.ExecuteQuery(sql);

            string[] months = new string[dt.Rows.Count];
            int[] bookingCounts = new int[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                months[i] = $"{row["Month"]}/{row["Year"]}";
                bookingCounts[i] = Convert.ToInt32(row["BookingCount"]);
            }

            string monthsJson = JsonConvert.SerializeObject(months);
            string bookingCountsJson = JsonConvert.SerializeObject(bookingCounts);

            string script = $@"
                <script>
                    var ctx = document.getElementById('monthlyBookingsChart').getContext('2d');
                    var chart = new Chart(ctx, {{
                        type: 'line',
                        data: {{
                            labels: {monthsJson},
                            datasets: [{{
                                label: 'Monthly Bookings',
                                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                                borderColor: 'rgba(75, 192, 192, 1)',
                                data: {bookingCountsJson}
                            }}]
                        }},
                        options: {{
                            scales: {{
                                yAxes: [{{
                                    ticks: {{
                                        beginAtZero: true
                                    }}
                                }}]
                            }}
                        }}
                    }});
                </script>";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "monthlyBookingsChart", script, false);
        }

        private void LoadIncomeStats()
        {
            // Total Income
            string sqlTotalIncome = "SELECT SUM(TotalCost) AS TotalIncome FROM Bookings";
            DataTable dtTotalIncome = dbFacade.ExecuteQuery(sqlTotalIncome);
            lblTotalIncome.Text = "Total Income: RM" + dtTotalIncome.Rows[0]["TotalIncome"].ToString();

            // Monthly Income
            string sqlMonthlyIncome = @"
                SELECT DATEPART(YEAR, StartDate) AS Year, DATEPART(MONTH, StartDate) AS Month, SUM(TotalCost) AS Income
                FROM Bookings
                GROUP BY DATEPART(YEAR, StartDate), DATEPART(MONTH, StartDate)
                ORDER BY Year, Month";

            DataTable dtMonthlyIncome = dbFacade.ExecuteQuery(sqlMonthlyIncome);
            gvMonthlyIncome.DataSource = dtMonthlyIncome;
            gvMonthlyIncome.DataBind();

            string[] months = new string[dtMonthlyIncome.Rows.Count];
            decimal[] incomes = new decimal[dtMonthlyIncome.Rows.Count];

            for (int i = 0; i < dtMonthlyIncome.Rows.Count; i++)
            {
                DataRow row = dtMonthlyIncome.Rows[i];
                months[i] = $"{row["Month"]}/{row["Year"]}";
                incomes[i] = Convert.ToDecimal(row["Income"]);
            }

            string monthsJson = JsonConvert.SerializeObject(months);
            string incomesJson = JsonConvert.SerializeObject(incomes);

            string script = $@"
                <script>
                    var ctx = document.getElementById('monthlyIncomeChart').getContext('2d');
                    var chart = new Chart(ctx, {{
                        type: 'bar',
                        data: {{
                            labels: {monthsJson},
                            datasets: [{{
                                label: 'Monthly Income',
                                backgroundColor: 'rgba(153, 102, 255, 0.2)',
                                borderColor: 'rgba(153, 102, 255, 1)',
                                data: {incomesJson}
                            }}]
                        }},
                        options: {{
                            scales: {{
                                yAxes: [{{
                                    ticks: {{
                                        beginAtZero: true
                                    }}
                                }}]
                            }}
                        }}
                    }});
                </script>";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "monthlyIncomeChart", script, false);

            // Income by Car
            string sqlIncomeByCar = @"
                SELECT c.Brand, c.Type, SUM(b.TotalCost) AS Income
                FROM Bookings b
                JOIN CarDetails c ON b.CarId = c.CarId
                GROUP BY c.Brand, c.Type
                ORDER BY Income DESC";

            DataTable dtIncomeByCar = dbFacade.ExecuteQuery(sqlIncomeByCar);
            gvIncomeByCar.DataSource = dtIncomeByCar;
            gvIncomeByCar.DataBind();
        }

        private void LoadFeedbackGrid()
        {
            string sql = "SELECT * FROM Bookings";
            DataTable dt = dbFacade.ExecuteQuery(sql);
            gvFeedback.DataSource = dt;
            gvFeedback.DataBind();
        }

        private void LoadTopUserAndRating()
        {
            string sqlTopUser = @"
                SELECT ua.Username, COUNT(b.BookingId) AS BookingCount
                FROM Bookings b
                JOIN UserAccount ua ON b.UserId = ua.UserId
                GROUP BY ua.Username
                ORDER BY BookingCount DESC";

            DataTable dtTopUser = dbFacade.ExecuteQuery(sqlTopUser);
            if (dtTopUser.Rows.Count > 0)
            {
                lblTopUser.Text = "Top User: " + dtTopUser.Rows[0]["Username"] + " with " + dtTopUser.Rows[0]["BookingCount"] + " bookings.";
            }

            string sqlTotalRating = "SELECT SUM(Rating) AS TotalRating FROM Bookings WHERE Rating IS NOT NULL";
            DataTable dtTotalRating = dbFacade.ExecuteQuery(sqlTotalRating);
            lblTotalRating.Text = "Total Rating: " + dtTotalRating.Rows[0]["TotalRating"].ToString();
        }

        private void LoadContactUsers()
        {
            string sql = @"
        SELECT DISTINCT ua.UserId, ua.Username, ua.Email, ua.Phone
        FROM Bookings b
        JOIN UserAccount ua ON b.UserId = ua.UserId
        WHERE b.Rating IS NOT NULL";

            DataTable dt = dbFacade.ExecuteQuery(sql);
            gvContactUsers.DataSource = dt;
            gvContactUsers.DataBind();
        }


        protected void gvContactUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ContactUser")
            {
                string email = e.CommandArgument.ToString();
                string subject = "Feedback on Your Recent Booking";
                string body = "Dear User,\n\nThank you for rating your recent booking with us. We would like to hear more about your experience. Please feel free to share any additional feedback.\n\nBest Regards,\nRent Rides Team";

                var emailService = new EmailServiceAdapter(new SmtpEmailService());
                try
                {
                    emailService.ContactUser(email, subject, body);
                    lblStatus.Text = $"Email sent successfully to {email}.";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    lblStatus.Text = $"Failed to send email to {email}. Error: {ex.Message}";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
        }


        private void LoadFeedbackAnalysis()
        {
            string sql = "SELECT Comment, Rating FROM Bookings WHERE Comment IS NOT NULL";
            DataTable dt = dbFacade.ExecuteQuery(sql);

            int positiveCount = 0, negativeCount = 0, neutralCount = 0;
            foreach (DataRow row in dt.Rows)
            {
                string comment = row["Comment"].ToString();
                int rating = Convert.ToInt32(row["Rating"]);

                if (rating >= 4)
                {
                    positiveCount++;
                }
                else if (rating == 3)
                {
                    neutralCount++;
                }
                else
                {
                    negativeCount++;
                }
            }

            lblPositiveFeedback.Text = $"Positive Feedback: {positiveCount}";
            lblNeutralFeedback.Text = $"Neutral Feedback: {neutralCount}";
            lblNegativeFeedback.Text = $"Negative Feedback: {negativeCount}";

            string[] sentiments = { "Positive", "Neutral", "Negative" };
            int[] sentimentCounts = { positiveCount, neutralCount, negativeCount };

            string sentimentsJson = JsonConvert.SerializeObject(sentiments);
            string sentimentCountsJson = JsonConvert.SerializeObject(sentimentCounts);

            string script = $@"
            <script>
                var ctx = document.getElementById('feedbackSentimentChart').getContext('2d');
                var chart = new Chart(ctx, {{
                    type: 'pie',
                    data: {{
                        labels: {sentimentsJson},
                        datasets: [{{
                            label: 'Feedback Sentiment',
                            data: {sentimentCountsJson},
                            backgroundColor: ['rgba(75, 192, 192, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(255, 99, 132, 0.2)'],
                            borderColor: ['rgba(75, 192, 192, 1)', 'rgba(255, 206, 86, 1)', 'rgba(255, 99, 132, 1)']
                        }}]
                    }},
                    options: {{
                        responsive: true,
                        maintainAspectRatio: false
                    }}
                }});
            </script>";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "feedbackSentimentChart", script, false);
        }
    }
}
