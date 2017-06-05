namespace Shuttle.Abacus.Events.Matrix.v1
{
    public class ConstraintAdded
    {
        public string Axis { get; set; }
        public int SequenceNumber { get; set; }
        public string Comparison { get; set; }
        public string Value { get; set; }
    }
}