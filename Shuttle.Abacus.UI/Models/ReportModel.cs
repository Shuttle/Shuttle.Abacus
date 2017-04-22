using System.Data;

namespace Shuttle.Abacus.UI.Models
{
    public class ReportModel
    {
        public DataTable ReportData { get; set; }

        public string ReportDatasetName { get; set; }

        public string ReportDefinitionName { get; set; }
    }
}
