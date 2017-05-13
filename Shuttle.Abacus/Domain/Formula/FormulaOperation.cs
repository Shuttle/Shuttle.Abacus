namespace Shuttle.Abacus.Domain
{
    public enum ValueSourceType
    {
        ArgumentAnswer = 0,
        Decimal = 1,
        RunningTotal = 2,
        FormulaTotal = 5,
        Matrix = 6,
        FormulaResult = 7
    }

    public class FormulaOperation
    {
        public FormulaOperation(int sequenceNumber, string operation, string valueSource, string valueSelection,
            string text)
        {
            SequenceNumber = sequenceNumber;
            Operation = operation;
            ValueSource = valueSource;
            ValueSelection = valueSelection;
            Text = text;
        }

        public int SequenceNumber { get; private set; }
        public string Operation { get; private set; }
        public string ValueSource { get; private set; }
        public string ValueSelection { get; private set; }
        public string Text { get; private set; }
    }
}