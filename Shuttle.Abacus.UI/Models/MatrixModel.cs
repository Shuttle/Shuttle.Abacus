using System;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Models
{
    public class MatrixModel
    {
        public MatrixModel(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            Id = MatrixColumns.MatrixId.MapFrom(row);
            Name = MatrixColumns.Name.MapFrom(row);
            RowArgumentName = MatrixColumns.RowArgumentName.MapFrom(row);
            ColumnArgumentName = MatrixColumns.RowArgumentName.MapFrom(row);
        }

        public MatrixModel()
        {
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RowArgumentName { get; set; }
        public string ColumnArgumentName { get; set; }
    }
}