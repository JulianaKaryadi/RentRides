using System.Data;
using Newtonsoft.Json;

namespace CarRental.strategies
{
    public class JsonReportStrategy : IReportStrategy
    {
        public string GenerateReport(DataTable dataTable)
        {
            return JsonConvert.SerializeObject(dataTable);
        }
    }
}
