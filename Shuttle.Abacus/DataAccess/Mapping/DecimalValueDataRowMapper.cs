using System.Data;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess
{
    public class DecimalValueDataRowMapper : IDataRowMapper<DecimalValue>
    {
        public DecimalValue MapFrom(DataRow row)
        {
            return new DecimalValue(
                DecimalValueColumns.Id.MapFrom(row),
                DecimalValueColumns.ColumnIndex.MapFrom(row),
                DecimalValueColumns.RowIndex.MapFrom(row),
                DecimalValueColumns.DecimalValue.MapFrom(row));
        }
    }
}
