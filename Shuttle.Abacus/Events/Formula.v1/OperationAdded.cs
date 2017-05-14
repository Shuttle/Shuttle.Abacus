namespace Shuttle.Abacus.Events.Formula.v1
{
    public class OperationAdded
    {
        public int SequenceNumber { get; set; }
        public string Operation { get; set; }
        public string ValueSource { get; set; }
        public string ValueSelection { get; set; }
    }
}