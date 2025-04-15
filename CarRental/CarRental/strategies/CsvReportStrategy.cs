using System.Data;
using System.IO;

namespace CarRental.strategies
{
    public class CsvReportStrategy : IReportStrategy
    {
        public string GenerateReport(DataTable dataTable)
        {
            StringWriter sw = new StringWriter();

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                sw.Write(dataTable.Columns[i]);
                if (i < dataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);

            foreach (DataRow dr in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    sw.Write(dr[i].ToString());
                    if (i < dataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }

            return sw.ToString();
        }
    }
}
