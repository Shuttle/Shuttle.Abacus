using Shuttle.Contract;

namespace Shuttle.Abacus;

public enum ValueProviderType
{
    Argument = 1,
    Decimal = 2,
    Matrix = 3,
    Algorithm = 4,
    Result = 5
}

public class AlgorithmOperation
{
    public AlgorithmOperation(Guid id, int sequenceNumber, string operation, string valueProviderName, string inputParameter)
    {
        Guard.AgainstEmpty(operation);
        Guard.AgainstEmpty(valueProviderName);
        Guard.AgainstEmpty(inputParameter);

        Id = id;
        SequenceNumber = sequenceNumber;
        Operation = operation;
        ValueProviderName = valueProviderName;
        InputParameter = inputParameter;
    }

    public Guid Id { get; }
    public int SequenceNumber { get; }
    public string Operation { get; }
    public string ValueProviderName { get; }
    public string InputParameter { get; }

    public string GetOperator()
    {
        return Operation.ToLowerInvariant() switch
        {
            "addition" => "+",
            "subtraction" => "-",
            "multiplication" => "*",
            "division" => "/",
            "rounding" => "rounded to",
            _ => $"(unknown operation {Operation})"
        };
    }

    public void Perform(AlgorithmContext context, decimal value)
    {
        switch (Operation.ToLowerInvariant())
        {
            case "addition":
                context.SetResult(context.Result + value);
                return;
            case "subtraction":
                context.SetResult(context.Result - value);
                return;
            case "multiplication":
                context.SetResult(context.Result * value);
                return;
            case "division":
                context.SetResult(context.Result / value);
                return;
            case "rounding":
                context.SetResult(Math.Round(context.Result, (int)value));
                return;
        }
    }
}
