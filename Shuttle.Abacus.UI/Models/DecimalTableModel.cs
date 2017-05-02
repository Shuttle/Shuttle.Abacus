using System;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Models
{
    public class DecimalTableModel
    {
        public DecimalTableModel(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            _row = row;
        }

        private readonly DataRow _row;

        public Guid Id => DecimalTableColumns.Id.MapFrom(_row);
        public string Name => DecimalTableColumns.Name.MapFrom(_row);
        public Guid RowArgumentId => DecimalTableColumns.RowArgumentId.MapFrom(_row);
        public Guid ColumnArgumentId => DecimalTableColumns.RowArgumentId.MapFrom(_row);
    }
}