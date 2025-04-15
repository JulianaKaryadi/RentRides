using System.Data;
using System.IO;
using System.Xml.Serialization;

namespace CarRental.strategies
{
    public class XmlReportStrategy : IReportStrategy
    {
        public string GenerateReport(DataTable dataTable)
        {
            if (string.IsNullOrEmpty(dataTable.TableName))
            {
                dataTable.TableName = "Bookings";
            }

            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
                serializer.Serialize(sw, dataTable);
                return sw.ToString();
            }
        }
    }
}
