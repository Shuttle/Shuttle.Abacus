using Shuttle.Contract;

namespace Shuttle.Abacus;

public class FormulaContext : IDisposable
{
    private readonly List<FormulaContext> _containedFormulaContexts = [];
    private readonly ExecutionContext _executionContext;
    private readonly List<ArgumentValue> _usedArgumentValues = [];

    public FormulaContext(ExecutionContext executionContext, string formulaName)
    {
        Guard.AgainstNull(executionContext);
        Guard.AgainstEmpty(formulaName);

        FormulaName = formulaName;
        _executionContext = executionContext;

        DateStarted = DateTime.Now;
    }

    public ConstraintViolation? ConstraintViolation { get; private set; }

    public DateTime DateStarted { get; }
    public DateTime DateCompleted { get; private set; }

    public string FormulaName { get; }
    public decimal Result { get; private set; }

    public double TotalMilliseconds => (DateCompleted - DateStarted).TotalMilliseconds;

    public void Dispose()
    {
        DateCompleted = DateTime.Now;

        _executionContext.FormulaContextCompleted(this);
    }

    public IEnumerable<FormulaContext> ContainedFormulaContexts()
    {
        return _containedFormulaContexts.AsReadOnly();
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

    public FormulaContext Disqualified(Argument argument, string argumentValue, string comparison, string constraintValue)
    {
        Guard.AgainstNull(argument);

        if (_executionContext.Logger.IsNormalEnabled)
        {
            _executionContext.Logger.LogNormal($"[disqualified] : {argument.Name} {comparison} {constraintValue} but was {argumentValue}");
        }

        ConstraintViolation = new(argument.Id, argumentValue, comparison, constraintValue);

        return this;
    }

    public void Add(FormulaContext formulaContext)
    {
        Guard.AgainstNull(formulaContext);

        _containedFormulaContexts.Add(formulaContext);
    }

    public void UsedArgumentValue(Guid argumentId, string value)
    {
        _usedArgumentValues.Add(new(argumentId, value));
    }
}
