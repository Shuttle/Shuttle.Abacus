using System.Data;
using Shuttle.Abacus.DataAccess;

namespace Shuttle.Abacus.UI.Models
{
    public class DecimalValueModel
    {
        public DataRow Row { get; }

        public DecimalValueModel(DataRow row)
        {
            Row = row;
        }

        public int ColumnIndex => DecimalValueColumns.ColumnIndex.MapFrom(Row);
        public int RowIndex => DecimalValueColumns.RowIndex.MapFrom(Row);
        public decimal DecimalValue => DecimalValueColumns.DecimalValue.MapFrom(Row);
    }
}