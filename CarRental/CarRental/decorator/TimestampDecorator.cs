using CarRental.strategies;
using System;
using System.Data;

namespace CarRental.decorator
{
    public class TimestampDecorator : ReportDecorator
    {
        public TimestampDecorator(IReportStrategy reportStrategy) : base(reportStrategy) { }

        public override string GenerateReport(DataTable dataTable)
        {
            string report = base.GenerateReport(dataTable);
            return $"{report}\nGenerated on: {DateTime.Now}";
        }
    }
}
