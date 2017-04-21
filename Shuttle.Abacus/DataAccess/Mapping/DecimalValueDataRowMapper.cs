using System.Data;
using Abacus.Domain;

namespace Abacus.Data
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
