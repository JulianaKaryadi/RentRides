using System;
using System.Net;
using System.Net.Mail;

namespace CarRental.adapter
{
    public class SmtpEmailService : IEmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("rentrides2u@gmail.com", "inwj qfyw pcxg xtoc"); 
                    smtpClient.EnableSsl = true; 

                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress("rentrides2u@gmail.com"),
                        Subject = subject,
                        Body = body
                    };
                    mailMessage.To.Add(to);

                    smtpClient.Send(mailMessage);
                    Console.WriteLine("Email sent successfully to {0}!", to);
                }
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"SMTP Exception: {smtpEx.Message}");
                throw; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception: {ex.Message}");
                throw; 
            }
        }
    }
}
