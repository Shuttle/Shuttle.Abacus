namespace Shuttle.Abacus;

public class ExecutionResult(string formulaName, decimal value, int depth)
{
    public string FormulaName { get; } = formulaName;
    public decimal Value { get; } = value;
    public int Depth { get; } = depth;
}
