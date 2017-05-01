using System.Collections.Generic;

namespace Shuttle.Abacus.DTO
{
    public class DecimalValueDTO
    {
        public int Row { get; set; }
        public string RowConstraintName { get; set; }
        public string RowConstraintAnswer { get; set; }
        public int Column { get; set; }
        public string ColumnConstraintName { get; set; }
        public string ColumnConstraintAnswer { get; set; }
        public decimal Value { get; set; }
    }
}
