using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Shuttle.Abacus.DataAccess
{
    public static class DataRowExtensions
    {
        public static DataRow GetRow(this IEnumerable<DataRow> rows)
        {
            if (!rows.Any())
            {
                throw new DataException();
            }

            return rows.FirstOrDefault();
        }
    }
}