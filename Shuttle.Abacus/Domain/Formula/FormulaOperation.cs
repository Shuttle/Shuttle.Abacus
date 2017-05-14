namespace Shuttle.Abacus.Domain
{
    public enum ValueSourceType
    {
        Argument = 1,
        Constant = 2,
        Matrix = 3,
        Formula = 4,
        RunningTotal = 5
    }

    public class FormulaOperation
    {
        public FormulaOperation(int sequenceNumber, string operation, string valueSource, string valueSelection)
        {
            SequenceNumber = sequenceNumber;
            Operation = operation;
            ValueSource = valueSource;
            ValueSelection = valueSelection;
        }

        public int SequenceNumber { get; private set; }
        public string Operation { get; private set; }
        public string ValueSource { get; private set; }
        public string ValueSelection { get; private set; }
    }
}