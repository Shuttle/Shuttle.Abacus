using Shuttle.Abacus.Events.Formula.v1;
using Shuttle.Contract;

namespace Shuttle.Abacus;

public class Formula
{
    private readonly List<FormulaConstraint> _constraints = [];
    private readonly List<FormulaOperation> _operations = [];

    public Guid Id { get; set; }

    public IEnumerable<FormulaOperation> Operations => _operations.AsReadOnly();

    public string Name { get; private set; } = string.Empty;

    // Dead code kept for reference: `RegisterFormulaMaximum`/`RegisterFormulaMinimum` participants throw
    // NotImplementedException, so these are never set by any event today.
    public string? MaximumFormulaName { get; private set; }
    public string? MinimumFormulaName { get; private set; }

    public IEnumerable<FormulaConstraint> Constraints => _constraints.AsReadOnly();

    public bool HasOperations => _operations.Count > 0;
    public bool Removed { get; private set; }

    public OperationRegistered RegisterOperation(Guid id, string operation, string valueProviderName, string inputParameter)
    {
        Guard.AgainstEmpty(operation);
        Guard.AgainstEmpty(valueProviderName);
        Guard.AgainstEmpty(inputParameter);

        return On(new OperationRegistered
        {
            Id = id,
            SequenceNumber = FindOperation(id)?.SequenceNumber ?? _operations.Count + 1,
            Operation = operation,
            ValueProviderName = valueProviderName,
            InputParameter = inputParameter
        });
    }

    public bool ContainsOperation(Guid id)
    {
        return FindOperation(id) != null;
    }

    public FormulaOperation? FindOperation(Guid id)
    {
        return _operations.Find(item => item.Id.Equals(id));
    }

    private OperationRegistered On(OperationRegistered operationRegistered)
    {
        Guard.AgainstNull(operationRegistered);

        _operations.RemoveAll(item => item.Id.Equals(operationRegistered.Id));

        _operations.Add(new(
            operationRegistered.Id,
            operationRegistered.SequenceNumber,
            operationRegistered.Operation,
            operationRegistered.ValueProviderName,
            operationRegistered.InputParameter));

        return operationRegistered;
    }

    public ConstraintRegistered RegisterConstraint(Guid id, Guid argumentId, string comparison, string value)
    {
        Guard.AgainstEmpty(comparison);
        Guard.AgainstEmpty(value);

        return On(new ConstraintRegistered
        {
            Id = id,
            ArgumentId = argumentId,
            Comparison = comparison,
            Value = value
        });
    }

    public bool ContainsConstraint(Guid id)
    {
        return FindConstraint(id) != null;
    }

    private FormulaConstraint? FindConstraint(Guid id)
    {
        return _constraints.Find(item => item.Id.Equals(id));
    }

    private ConstraintRegistered On(ConstraintRegistered constraintRegistered)
    {
        Guard.AgainstNull(constraintRegistered);

        _constraints.RemoveAll(item => item.Id.Equals(constraintRegistered.Id));

        _constraints.Add(new(
            constraintRegistered.Id,
            constraintRegistered.ArgumentId,
            constraintRegistered.Comparison,
            constraintRegistered.Value));

        return constraintRegistered;
    }

    public Registered Register(string name)
    {
        Guard.AgainstEmpty(name);

        return On(new Registered
        {
            Name = name
        });
    }

    private Registered On(Registered registered)
    {
        Guard.AgainstNull(registered);

        Name = registered.Name;

        return registered;
    }

    public Removed Remove()
    {
        if (Removed)
        {
            throw new DomainException("Already removed.");
        }

        return On(new Removed());
    }

    private Removed On(Removed removed)
    {
        Guard.AgainstNull(removed);

        Removed = true;

        return removed;
    }

    public bool IsNamed(string name)
    {
        Guard.AgainstEmpty(name);

        return Name.Equals(name, StringComparison.InvariantCultureIgnoreCase);
    }

    public Renamed Rename(string name)
    {
        Guard.AgainstEmpty(name);

        if (IsNamed(name))
        {
            throw new DomainException($"Already named '{name}'.");
        }

        return On(new Renamed
        {
            Name = name
        });
    }

    private Renamed On(Renamed renamed)
    {
        Guard.AgainstNull(renamed);

        Name = renamed.Name;

        return renamed;
    }

    public static string Key(string name)
    {
        return $"[formula]:name={name}";
    }

    public OperationRemoved RemoveOperation(Guid id)
    {
        var operation = FindOperation(id);

        if (operation == null)
        {
            throw new DomainException(Resources.MissingItem);
        }

        return On(new OperationRemoved
        {
            Id = id,
            SequenceNumber = operation.SequenceNumber
        });
    }

    private OperationRemoved On(OperationRemoved operationRemoved)
    {
        Guard.AgainstNull(operationRemoved);

        _operations.RemoveAll(item => item.Id.Equals(operationRemoved.Id));

        return operationRemoved;
    }

    public ConstraintRemoved RemoveConstraint(Guid id)
    {
        return On(new ConstraintRemoved
        {
            Id = id
        });
    }

    private ConstraintRemoved On(ConstraintRemoved constraintRemoved)
    {
        Guard.AgainstNull(constraintRemoved);

        _constraints.RemoveAll(item => item.Id.Equals(constraintRemoved.Id));

        return constraintRemoved;
    }
}
