using System;

namespace Shuttle.Abacus.WebApi
{
    public class MatrixConstraintModel
    {
        public Guid Id { get; set; }
        public Guid MatrixId { get; set; }
        public string Axis { get; set; }
        public int Index { get; set; }
        public string Comparison { get; set; }
        public string Value { get; set; }

        public bool Ok()
        {
            return !MatrixId.Equals(Guid.Empty) &&
                   !string.IsNullOrEmpty(Axis) &&
                   !(Axis.Equals("Row", StringComparison.InvariantCultureIgnoreCase) ||
                     Axis.Equals("Column", StringComparison.InvariantCultureIgnoreCase)) &&
                   Index > 0 &&
                   !string.IsNullOrEmpty(Comparison) &&
                   !string.IsNullOrEmpty(Value);
        }
    }
}