using CarRental.strategies;
using System.Data;

namespace CarRental.decorator
{
    public abstract class ReportDecorator : IReportStrategy
    {
        protected IReportStrategy _reportStrategy;

        public ReportDecorator(IReportStrategy reportStrategy)
        {
            _reportStrategy = reportStrategy;
        }

        public virtual string GenerateReport(DataTable dataTable)
        {
            return _reportStrategy.GenerateReport(dataTable);
        }
    }
}
