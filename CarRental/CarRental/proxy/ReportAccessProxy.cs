using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.proxy
{
    public class ReportAccessProxy : IReportAccess
    {
        private readonly string _userRole;

        public ReportAccessProxy(string userRole)
        {
            _userRole = userRole;
        }

        public bool CanGenerateReport(string userRole)
        {
            return _userRole == "Admin";
        }
    }
}
