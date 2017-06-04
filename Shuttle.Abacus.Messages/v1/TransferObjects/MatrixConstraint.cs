namespace Shuttle.Abacus.Messages.v1.TransferObjects
{
    public class MatrixConstraint
    {
        public string Axis { get; set; }
        public int SequenceNumber { get; set; }
        public string Comparison { get; set; }
        public string Value { get; set; }
    }
}