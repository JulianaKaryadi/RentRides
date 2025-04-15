using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.proxy
{
    public interface IReportAccess
    {
        bool CanGenerateReport(string userRole);
    }
}
