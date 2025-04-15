using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.adapter
{
    public class EmailServiceAdapter
    {
        private readonly IEmailService _emailService;

        public EmailServiceAdapter(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void ContactUser(string email, string subject, string body)
        {
            _emailService.SendEmail(email, subject, body);
        }
    }
}
