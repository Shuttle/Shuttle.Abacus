using System;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Models
{
    public class MatrixModel
    {
        public MatrixModel(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            _row = row;
        }

        private readonly DataRow _row;

        public Guid Id => MatrixColumns.Id.MapFrom(_row);
        public string Name => MatrixColumns.Name.MapFrom(_row);
        public Guid RowArgumentId => MatrixColumns.RowArgumentId.MapFrom(_row);
        public Guid ColumnArgumentId => MatrixColumns.RowArgumentId.MapFrom(_row);
    }
}