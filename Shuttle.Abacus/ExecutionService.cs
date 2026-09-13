using System.Globalization;
using Shuttle.Contract;

namespace Shuttle.Abacus;

public class ExecutionService : IExecutionService
{
    private readonly object _lock = new();
    private readonly IAlgorithmRepository _algorithmRepository;
    private readonly IArgumentRepository _argumentRepository;
    private readonly IMatrixRepository _matrixRepository;
    private readonly Dictionary<Guid, Argument> _arguments = new();
    private readonly IValueComparer _valueComparer;
    private readonly Dictionary<Guid, Algorithm> _algorithms = new();
    private readonly Dictionary<Guid, Matrix> _matrices = new();
    private bool _initialized;

    public ExecutionService(IValueComparer valueComparer, IAlgorithmRepository algorithmRepository,
        IArgumentRepository argumentRepository, IMatrixRepository matrixRepository)
    {
        Guard.AgainstNull(valueComparer);
        Guard.AgainstNull(algorithmRepository);
        Guard.AgainstNull(argumentRepository);
        Guard.AgainstNull(matrixRepository);

        _valueComparer = valueComparer;
        _algorithmRepository = algorithmRepository;
        _argumentRepository = argumentRepository;
        _matrixRepository = matrixRepository;
    }

    public IExecutionService Flush()
    {
        lock (_lock)
        {
            _algorithms.Clear();
            _arguments.Clear();
            _matrices.Clear();

            _initialized = false;
        }

        return this;
    }

    public IExecutionService AddMatrix(Matrix matrix)
    {
        Guard.AgainstNull(matrix);

        lock (_lock)
        {
            _matrices.TryAdd(matrix.Id, matrix);
        }

        return this;
    }

    public IExecutionService AddArgument(Argument argument)
    {
        Guard.AgainstNull(argument);

        lock (_lock)
        {
            _arguments.TryAdd(argument.Id, argument);
        }

        return this;
    }

    public IExecutionService AddAlgorithm(Algorithm algorithm)
    {
        Guard.AgainstNull(algorithm);

        lock (_lock)
        {
            _algorithms.TryAdd(algorithm.Id, algorithm);
        }

        return this;
    }

    public async Task<ExecutionContext> ExecuteAsync(Guid algorithmId, IEnumerable<ArgumentValue> argumentValues, IContextLogger logger, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(argumentValues);
        Guard.AgainstNull(logger);

        await InitializeAsync(cancellationToken);

        var values = argumentValues as ArgumentValue[] ?? argumentValues.ToArray();
        var context = new ExecutionContext(values, logger);

        foreach (var argumentValue in values)
        {
            var argument = GetArgument(argumentValue.Id);

            if (logger.LogLevel == ContextLogLevel.Verbose)
            {
                logger.LogVerbose($"[inputParameter] {argumentValue.Id} / {argument.Name} = '{argumentValue.Value}'");
            }
        }

        try
        {
            Execute(context, algorithmId, logger);
        }
        catch (Exception ex)
        {
            context.WithException(ex);
        }

        return context;
    }

    private async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (_initialized)
        {
            return;
        }

        foreach (var algorithm in await _algorithmRepository.AllAsync(cancellationToken))
        {
            AddAlgorithm(algorithm);
        }

        foreach (var argument in await _argumentRepository.AllAsync(cancellationToken))
        {
            AddArgument(argument);
        }

        foreach (var matrix in await _matrixRepository.AllAsync(cancellationToken))
        {
            AddMatrix(matrix);
        }

        lock (_lock)
        {
            _initialized = true;
        }
    }

    private AlgorithmContext Execute(ExecutionContext executionContext, Guid algorithmId, IContextLogger logger)
    {
        var algorithm = GetAlgorithm(algorithmId);

        executionContext.CyclicInvariant(algorithm.Name);

        using var algorithmContext = executionContext.AlgorithmContext(algorithm.Name);

        foreach (var constraint in algorithm.Constraints)
        {
            var argument = GetArgument(constraint.ArgumentId);
            var argumentValue = executionContext.GetArgumentValue(constraint.ArgumentId);

            if (!_valueComparer.IsSatisfiedBy(argument.DataType, argumentValue, constraint.Comparison, constraint.Value))
            {
                if (logger.LogLevel == ContextLogLevel.Verbose)
                {
                    logger.LogVerbose($"[disqualified] {argument.Name} is '{argumentValue}' and should {constraint.Comparison} '{constraint.Value}'");
                }

                return algorithmContext.Disqualified(argument, argumentValue, constraint.Comparison, constraint.Value);
            }
        }

        foreach (var operation in algorithm.Operations)
        {
            decimal value = 0;

            switch (operation.ValueProviderName.ToLowerInvariant())
            {
                case "decimal":
                    value = Convert.ToDecimal(operation.InputParameter, CultureInfo.InvariantCulture);
                    break;
                case "argument":
                    value = Convert.ToDecimal(executionContext.GetArgumentValue(new(operation.InputParameter)), CultureInfo.InvariantCulture);
                    break;
                case "matrix":
                    var matrix = GetMatrix(new(operation.InputParameter));

                    value = Convert.ToDecimal(matrix.GetValue(_valueComparer, executionContext,
                        GetArgument(matrix.RowArgumentId),
                        matrix.ColumnArgumentId.HasValue ? GetArgument(matrix.ColumnArgumentId.Value) : null), CultureInfo.InvariantCulture);

                    break;
                case "algorithm":
                    value = Execute(executionContext, new(operation.InputParameter), logger).Result;
                    break;
                case "result":
                    value = algorithmContext.Result;
                    break;
            }

            if (logger.LogLevel == ContextLogLevel.Verbose)
            {
                logger.LogVerbose($"[operation] {algorithmContext.Result} {operation.GetOperator()} {value}");
            }

            operation.Perform(algorithmContext, value);
        }

        return algorithmContext;
    }

    private Matrix GetMatrix(Guid id)
    {
        lock (_lock)
        {
            if (!_matrices.TryGetValue(id, out var matrix))
            {
                throw new InvalidOperationException($"There is no matrix with id '{id}'.");
            }

            return matrix;
        }
    }

    private Algorithm GetAlgorithm(Guid id)
    {
        lock (_lock)
        {
            if (!_algorithms.TryGetValue(id, out var algorithm))
            {
                throw new InvalidOperationException($"There is no algorithm with id '{id}'.");
            }

            return algorithm;
        }
    }

    private Argument GetArgument(Guid id)
    {
        lock (_lock)
        {
            if (!_arguments.TryGetValue(id, out var argument))
            {
                throw new InvalidOperationException($"There is no argument with name '{id}'.");
            }

            return argument;
        }
    }
}
