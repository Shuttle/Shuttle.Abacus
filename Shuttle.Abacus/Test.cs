using Shuttle.Abacus.Events.Test.v1;
using Shuttle.Contract;

namespace Shuttle.Abacus;

public class Test
{
    private readonly List<ArgumentValue> _values = [];

    public Guid Id { get; set; }

    public string Name { get; private set; } = string.Empty;
    public Guid AlgorithmId { get; private set; }
    public string ExpectedResult { get; private set; } = string.Empty;
    public string ExpectedResultDataTypeName { get; private set; } = string.Empty;
    public string Comparison { get; private set; } = string.Empty;
    public bool Removed { get; private set; }

    public Registered Register(string name, Guid algorithmId, string expectedResult, string expectedResultDataTypeName, string comparison)
    {
        Guard.AgainstEmpty(name);
        Guard.AgainstEmpty(expectedResult);
        Guard.AgainstEmpty(expectedResultDataTypeName);
        Guard.AgainstEmpty(comparison);

        return On(new Registered
        {
            Name = name,
            AlgorithmId = algorithmId,
            ExpectedResult = expectedResult,
            ExpectedResultDataTypeName = expectedResultDataTypeName,
            Comparison = comparison
        });
    }

    private Registered On(Registered registered)
    {
        Guard.AgainstNull(registered);

        Name = registered.Name;
        AlgorithmId = registered.AlgorithmId;
        ExpectedResult = registered.ExpectedResult;
        ExpectedResultDataTypeName = registered.ExpectedResultDataTypeName;
        Comparison = registered.Comparison;

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
        return $"[test]:name={name}";
    }

    public ArgumentRegistered RegisterArgument(Guid argumentId, string value)
    {
        Guard.AgainstEmpty(value);

        return On(new ArgumentRegistered
        {
            ArgumentId = argumentId,
            Value = value
        });
    }

    private ArgumentRegistered On(ArgumentRegistered argumentRegistered)
    {
        Guard.AgainstNull(argumentRegistered);

        var existing = FindValue(argumentRegistered.ArgumentId);

        if (existing != null)
        {
            _values.Remove(existing);
        }

        _values.Add(new(argumentRegistered.ArgumentId, argumentRegistered.Value));

        return argumentRegistered;
    }

    private ArgumentValue? FindValue(Guid argumentId)
    {
        return _values.Find(argumentValue => argumentValue.Id.Equals(argumentId));
    }

    public ArgumentRemoved RemoveArgument(Guid argumentId)
    {
        return On(new ArgumentRemoved
        {
            ArgumentId = argumentId
        });
    }

    private ArgumentRemoved On(ArgumentRemoved argumentRemoved)
    {
        Guard.AgainstNull(argumentRemoved);

        var existing = FindValue(argumentRemoved.ArgumentId);

        if (existing != null)
        {
            _values.Remove(existing);
        }

        return argumentRemoved;
    }

    public IEnumerable<ArgumentValue> ArgumentValues()
    {
        return _values.AsReadOnly();
    }
}
