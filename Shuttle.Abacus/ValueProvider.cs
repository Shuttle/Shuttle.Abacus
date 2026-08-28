using Shuttle.Contract;

namespace Shuttle.Abacus;

public class ValueProvider<T> : IValueProvider
{
    private readonly Dictionary<string, T> _values = new();

    public ValueProvider(string name)
    {
        Guard.AgainstEmpty(name);

        Name = name;
    }

    public string Name { get; }

    public string Value(string inputParameter)
    {
        throw new NotImplementedException();
    }

    public IValueProvider Add(string name, T value)
    {
        throw new NotImplementedException();
    }
}
