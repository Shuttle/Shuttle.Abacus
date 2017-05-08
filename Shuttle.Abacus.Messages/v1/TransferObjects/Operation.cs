namespace Shuttle.Abacus.Messages.v1.TransferObjects
{
    public class FormulaOperation
    {
        public int SequenceNumber { get; set; }
        public string Operation { get; set; }
        public string ValueSource { get; set; }
        public string ValueSelection { get; set; }
        public string Text { get; set; }
    }
}