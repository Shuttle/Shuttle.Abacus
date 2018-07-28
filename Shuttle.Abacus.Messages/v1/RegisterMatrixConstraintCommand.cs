using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterMatrixConstraintCommand
    {
        public Guid MatrixId { get; set; }
        public Guid Id { get; set; }
        public string Axis { get; set; }
        public int Index { get; set; }
        public string Comparison { get; set; }
        public string Value { get; set; }
    }
}