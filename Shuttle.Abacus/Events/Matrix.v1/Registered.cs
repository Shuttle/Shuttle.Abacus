using System;

namespace Shuttle.Abacus.Events.Matrix.v1
{
    public class Registered
    {
        public string Name { get; set; }
        public Guid RowArgumentId { get; set; }
        public Guid? ColumnArgumentId { get; set; }
        public string DataTypeName { get; set; }
    }
}