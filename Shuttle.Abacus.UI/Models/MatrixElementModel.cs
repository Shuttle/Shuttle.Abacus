using System.Data;
using Shuttle.Abacus.DataAccess;

namespace Shuttle.Abacus.UI.Models
{
    public class MatrixElementModel
    {
        public DataRow Row { get; }

        public MatrixElementModel(DataRow row)
        {
            Row = row;
        }

        public int ColumnIndex => MatrixColumns.ElementColumns.ColumnIndex.MapFrom(Row);
        public int RowIndex => MatrixColumns.ElementColumns.RowIndex.MapFrom(Row);
        public decimal DecimalValue => MatrixColumns.ElementColumns.DecimalValue.MapFrom(Row);
    }
}