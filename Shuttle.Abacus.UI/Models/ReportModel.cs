using System.Data;

namespace Abacus.UI
{
    public class ReportModel
    {
        public DataTable ReportData { get; set; }

        public string ReportDatasetName { get; set; }

        public string ReportDefinitionName { get; set; }
    }
}
