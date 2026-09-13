using System.Text;
using Shuttle.Contract;

namespace Shuttle.Abacus;

public class ExecutionContext
{
    private readonly List<ExecutionResult> _results = [];
    private readonly Stack<AlgorithmContext> _stack = new();

    private readonly Dictionary<Guid, string> _values = new();

    public ExecutionContext(IEnumerable<ArgumentValue> values, IContextLogger logger)
    {
        Guard.AgainstNull(values);
        Guard.AgainstNull(logger);

        Logger = logger;

        foreach (var argumentValue in values)
        {
            _values.Add(argumentValue.Id, argumentValue.Value);
        }
    }

    public bool HasActiveAlgorithmContext => _stack.Count > 0;

    public AlgorithmContext? RootAlgorithmContext { get; private set; }

    public Exception? Exception { get; private set; }

    public bool HasException => Exception != null;
    public IContextLogger Logger { get; }

    public string GetArgumentValue(Guid id)
    {
        if (!_values.TryGetValue(id, out var result))
        {
            throw new InvalidOperationException($"There is no argument value with id '{id}'.");
        }

        if (HasActiveAlgorithmContext)
        {
            ActiveAlgorithmContext()!.UsedArgumentValue(id, result);
        }

        return result;
    }

    public int Depth()
    {
        return _stack.Count;
    }

    public AlgorithmContext AlgorithmContext(string algorithmName)
    {
        Guard.AgainstEmpty(algorithmName);

        var result = new AlgorithmContext(this, algorithmName);

        if (_stack.Count > 0)
        {
            _stack.Peek().Add(result);
        }
        else
        {
            RootAlgorithmContext = result;
        }

        _stack.Push(result);

        Logger.LogNormal($"[starting] : {algorithmName}");

        return result;
    }

    public void AlgorithmContextCompleted(AlgorithmContext algorithmContext)
    {
        Guard.AgainstNull(algorithmContext);

        AddResult(algorithmContext);

        Logger.LogNormal($"[completed] : {algorithmContext.AlgorithmName} ({algorithmContext.TotalMilliseconds} ms)");

        if (_stack.Count > 0)
        {
            _stack.Pop();
        }
    }

    private void AddResult(AlgorithmContext algorithmContext)
    {
        Guard.AgainstNull(algorithmContext);

        Logger.LogNormal($"[result] : {algorithmContext.Result} ({algorithmContext.AlgorithmName})");

        AddResult(algorithmContext.AlgorithmName, algorithmContext.Result);
    }

    public void AddResult(string algorithmName, decimal result)
    {
        Guard.AgainstEmpty(algorithmName);

        _results.Add(new(algorithmName, result, Depth()));
    }

    public ExecutionResult? RootResult()
    {
        return _results.FirstOrDefault();
    }

    public decimal GetResult()
    {
        return RootResult()?.Value ?? 0;
    }

    public IEnumerable<ExecutionResult> GetResults()
    {
        return _results.AsReadOnly();
    }

    public void CyclicInvariant(string algorithmName)
    {
        Guard.AgainstEmpty(algorithmName);

        var cyclic = _stack.Any(context => context.AlgorithmName.Equals(algorithmName, StringComparison.InvariantCultureIgnoreCase));

        if (!cyclic)
        {
            return;
        }

        var stack = new StringBuilder(algorithmName);

        foreach (var context in _stack)
        {
            stack.Append($" <- {context.AlgorithmName}");
        }

        throw new InvalidOperationException($"Cyclic algorithm usage: {stack}");
    }

    public ExecutionContext WithException(Exception exception)
    {
        Exception = exception;

        return this;
    }

    public AlgorithmContext? ActiveAlgorithmContext()
    {
        return _stack.Count == 0 ? null : _stack.Peek();
    }
}
