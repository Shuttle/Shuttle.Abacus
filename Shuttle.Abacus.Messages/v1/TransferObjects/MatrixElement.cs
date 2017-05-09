using System.Collections.Generic;

namespace Shuttle.Abacus.Messages.v1.TransferObjects
{
    public class MatrixElement
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public decimal Value { get; set; }
        public List<Constraint> Constraints { get; set; }
    }
}