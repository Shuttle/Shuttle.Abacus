namespace Shuttle.Abacus.Events.Formula.v1
{
    public class ConstraintAdded
    {
        public int SequenceNumber { get; set; }
        public string ArgumentName { get; set; }
        public string Comparison { get; set; }
        public string Value { get; set; }
    }
}