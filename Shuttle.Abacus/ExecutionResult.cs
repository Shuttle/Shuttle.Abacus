namespace Shuttle.Abacus;

public class ExecutionResult(string algorithmName, decimal value, int depth)
{
    public string AlgorithmName { get; } = algorithmName;
    public decimal Value { get; } = value;
    public int Depth { get; } = depth;
}
