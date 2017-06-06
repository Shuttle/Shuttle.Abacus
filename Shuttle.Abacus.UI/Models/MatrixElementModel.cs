using System.Data;
using Shuttle.Abacus.DataAccess;

namespace Shuttle.Abacus.Shell.Models
{
    public class MatrixElementModel
    {
        public DataRow Row { get; }

        public MatrixElementModel(DataRow row)
        {
            Row = row;
        }

        public int ColumnIndex => MatrixColumns.ElementColumns.Column.MapFrom(Row);
        public int RowIndex => MatrixColumns.ElementColumns.Row.MapFrom(Row);
        public decimal DecimalValue => MatrixColumns.ElementColumns.Value.MapFrom(Row);
    }
}