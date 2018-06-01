namespace Shuttle.Abacus
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

        public int SequenceNumber { get; }
        public string Operation { get; }
        public string ValueSource { get; }
        public string ValueSelection { get; }

        public void Perform(FormulaContext context, decimal value)
        {
            switch (Operation.ToLower())
            {
                case "addition":
                {
                    context.SetResult(context.Result + value);

                    return;
                }
            }
        }
    }
}