using Shuttle.Contract;

namespace Shuttle.Abacus;

public class AlgorithmContext : IDisposable
{
    private readonly List<AlgorithmContext> _containedAlgorithmContexts = [];
    private readonly ExecutionContext _executionContext;
    private readonly List<ArgumentValue> _usedArgumentValues = [];

    public AlgorithmContext(ExecutionContext executionContext, string algorithmName)
    {
        Guard.AgainstNull(executionContext);
        Guard.AgainstEmpty(algorithmName);

        AlgorithmName = algorithmName;
        _executionContext = executionContext;

        DateStarted = DateTime.Now;
    }

    public ConstraintViolation? ConstraintViolation { get; private set; }

    public DateTime DateStarted { get; }
    public DateTime DateCompleted { get; private set; }

    public string AlgorithmName { get; }
    public decimal Result { get; private set; }

    public double TotalMilliseconds => (DateCompleted - DateStarted).TotalMilliseconds;

    public void Dispose()
    {
        DateCompleted = DateTime.Now;

        _executionContext.AlgorithmContextCompleted(this);
    }

    public IEnumerable<AlgorithmContext> ContainedAlgorithmContexts()
    {
        return _containedAlgorithmContexts.AsReadOnly();
    }

    public IEnumerable<ArgumentValue> UsedArgumentValues()
    {
        return _usedArgumentValues.AsReadOnly();
    }

    public decimal ZeroResult()
    {
        Result = 0;
        return 0;
    }

    public void SetResult(decimal result)
    {
        Result = result;
    }

    public AlgorithmContext Disqualified(Argument argument, string argumentValue, string comparison, string constraintValue)
    {
        Guard.AgainstNull(argument);

        if (_executionContext.Logger.IsNormalEnabled)
        {
            _executionContext.Logger.LogNormal($"[disqualified] : {argument.Name} {comparison} {constraintValue} but was {argumentValue}");
        }

        ConstraintViolation = new(argument.Id, argumentValue, comparison, constraintValue);

        return this;
    }

    public void Add(AlgorithmContext algorithmContext)
    {
        Guard.AgainstNull(algorithmContext);

        _containedAlgorithmContexts.Add(algorithmContext);
    }

    public void UsedArgumentValue(Guid argumentId, string value)
    {
        _usedArgumentValues.Add(new(argumentId, value));
    }
}
