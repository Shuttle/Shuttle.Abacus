using System;

namespace Shuttle.Abacus.WebApi
{
    public class MatrixModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid RowArgumentId { get; set; }
        public Guid? ColumnArgumentId { get; set; }
        public string DataTypeName { get; set; }
    }
}