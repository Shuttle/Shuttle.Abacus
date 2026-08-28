using Shuttle.Contract;

namespace Shuttle.Abacus;

public class ArgumentValueProvider
{
    private readonly Dictionary<string, string> _arguments = new();

    public ArgumentValueProvider Add(string name, string value)
    {
        Guard.AgainstEmpty(name);

        _arguments.Add(name, value);

        return this;
    }

    public string GetValue(string name)
    {
        if (!_arguments.TryGetValue(name, out var value))
        {
            throw new InvalidOperationException($"Could not find an argument with name '{name}'.");
        }

        return value;
    }
}
