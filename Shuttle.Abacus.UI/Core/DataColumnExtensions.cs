using System.Data;
using System.Threading;

namespace Shuttle.Abacus.Shell.Core
{
    public static class DataColumnExtensions
    {
        public static string Text(this DataColumn column)
        {
            var result = Localisation.Resources.ResourceManager.GetString("ColumnText_" + column.ColumnName,
                Thread.CurrentThread.CurrentUICulture);

            if (string.IsNullOrEmpty(result))
            {
                result = string.Format("[{0}]", column.ColumnName);
            }

            return result;
        }
    }
}