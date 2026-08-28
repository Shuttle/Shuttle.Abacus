using Shuttle.Abacus.Events.Argument.v1;
using Shuttle.Contract;

namespace Shuttle.Abacus;

public class Argument
{
    private readonly List<string> _values = [];

    // Set by the repository/participant that loads this aggregate from the event stream — Recall's
    // `EventStream.Get<T>()` requires a parameterless constructor and knows nothing of this domain-specific id,
    // but `ExecutionService` needs to key bulk-loaded collections of arguments/formulas/matrices by it.
    public Guid Id { get; set; }

    public string Name { get; private set; } = string.Empty;
    public string DataType { get; private set; } = string.Empty;
    public bool Removed { get; private set; }

    public IEnumerable<string> Values => _values.AsReadOnly();
    public bool HasValues => _values.Count > 0;

    public Registered Register(string name, string dataType)
    {
        Guard.AgainstEmpty(name);
        Guard.AgainstEmpty(dataType);

        return On(new Registered
        {
            Name = name,
            DataTypeName = dataType
        });
    }

    private Registered On(Registered registered)
    {
        Guard.AgainstNull(registered);

        Name = registered.Name;
        DataType = registered.DataTypeName;

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

    // Dead code kept for reference: no participant currently calls this — `DataType` is only ever set at
    // registration time — but the event/handler plumbing may be wired up for it later.
    public DataTypeNameSet SetDataTypeName(string dataType)
    {
        Guard.AgainstEmpty(dataType);

        return On(new DataTypeNameSet
        {
            DataTypeName = dataType
        });
    }

    private DataTypeNameSet On(DataTypeNameSet dataTypeNameSet)
    {
        Guard.AgainstNull(dataTypeNameSet);

        DataType = dataTypeNameSet.DataTypeName;

        return dataTypeNameSet;
    }

    public ValueAdded AddValue(string value)
    {
        if (ContainsValue(value))
        {
            throw new DomainException($"Value '{value}' has already been added.");
        }

        return On(new ValueAdded
        {
            Value = value
        });
    }

    private ValueAdded On(ValueAdded valueAdded)
    {
        Guard.AgainstNull(valueAdded);

        _values.Add(valueAdded.Value);

        return valueAdded;
    }

    public bool ContainsValue(string value)
    {
        return _values.Contains(value);
    }

    public static string Key(string name)
    {
        return $"[argument]:name={name}";
    }

    public ValueRemoved RemoveValue(string value)
    {
        if (!ContainsValue(value))
        {
            throw new DomainException($"Cannot remove value '{value}' since it does not exist.");
        }

        return On(new ValueRemoved
        {
            Value = value
        });
    }

    private ValueRemoved On(ValueRemoved valueRemoved)
    {
        Guard.AgainstNull(valueRemoved);

        _values.Remove(valueRemoved.Value);

        return valueRemoved;
    }
}
