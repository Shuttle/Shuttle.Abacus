using System.Data;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class DecimalValueDataRowMapper : IDataRowMapper<DecimalValue>
    {
        public MappedRow<DecimalValue> Map(DataRow row)
        {
            var result = new DecimalValue(
                DecimalValueColumns.Id.MapFrom(row),
                DecimalValueColumns.ColumnIndex.MapFrom(row),
                DecimalValueColumns.RowIndex.MapFrom(row),
                DecimalValueColumns.DecimalValue.MapFrom(row));

            return new MappedRow<DecimalValue>(row, result);
        }
    }
}
