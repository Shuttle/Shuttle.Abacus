using System;

namespace Shuttle.Abacus.Events.Matrix.v1
{
    public class ConstraintRegistered
    {
        public Guid Id { get; set; }
        public string Axis { get; set; }
        public int Index { get; set; }
        public string Comparison { get; set; }
        public string Value { get; set; }
    }
}