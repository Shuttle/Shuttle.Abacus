namespace Shuttle.Abacus
{
    public class ExecutionResult
    {
        public string FormulaName { get; }
        public decimal Value { get; }
        public int Depth { get; }

        public ExecutionResult(string formulaName, decimal value, int depth)
        {
            FormulaName = formulaName;
            Value = value;
            Depth = depth;
        }
    }
}