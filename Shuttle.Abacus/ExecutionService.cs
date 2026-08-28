using System.Globalization;
using Shuttle.Contract;

namespace Shuttle.Abacus;

public class ExecutionService : IExecutionService
{
    private readonly object _lock = new();
    private readonly IFormulaRepository _formulaRepository;
    private readonly IArgumentRepository _argumentRepository;
    private readonly IMatrixRepository _matrixRepository;
    private readonly Dictionary<Guid, Argument> _arguments = new();
    private readonly IValueComparer _valueComparer;
    private readonly Dictionary<Guid, Formula> _formulas = new();
    private readonly Dictionary<Guid, Matrix> _matrices = new();
    private bool _initialized;

    public ExecutionService(IValueComparer valueComparer, IFormulaRepository formulaRepository,
        IArgumentRepository argumentRepository, IMatrixRepository matrixRepository)
    {
        Guard.AgainstNull(valueComparer);
        Guard.AgainstNull(formulaRepository);
        Guard.AgainstNull(argumentRepository);
        Guard.AgainstNull(matrixRepository);

        _valueComparer = valueComparer;
        _formulaRepository = formulaRepository;
        _argumentRepository = argumentRepository;
        _matrixRepository = matrixRepository;
    }

    public IExecutionService Flush()
    {
        lock (_lock)
        {
            _formulas.Clear();
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

    public IExecutionService AddFormula(Formula formula)
    {
        Guard.AgainstNull(formula);

        lock (_lock)
        {
            _formulas.TryAdd(formula.Id, formula);
        }

        return this;
    }

    public async Task<ExecutionContext> ExecuteAsync(Guid formulaId, IEnumerable<ArgumentValue> argumentValues, IContextLogger logger, CancellationToken cancellationToken = default)
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
            Execute(context, formulaId, logger);
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

        foreach (var formula in await _formulaRepository.AllAsync(cancellationToken))
        {
            AddFormula(formula);
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

    private FormulaContext Execute(ExecutionContext executionContext, Guid formulaId, IContextLogger logger)
    {
        var formula = GetFormula(formulaId);

        executionContext.CyclicInvariant(formula.Name);

        using var formulaContext = executionContext.FormulaContext(formula.Name);

        foreach (var constraint in formula.Constraints)
        {
            var argument = GetArgument(constraint.ArgumentId);
            var argumentValue = executionContext.GetArgumentValue(constraint.ArgumentId);

            if (!_valueComparer.IsSatisfiedBy(argument.DataType, argumentValue, constraint.Comparison, constraint.Value))
            {
                if (logger.LogLevel == ContextLogLevel.Verbose)
                {
                    logger.LogVerbose($"[disqualified] {argument.Name} is '{argumentValue}' and should {constraint.Comparison} '{constraint.Value}'");
                }

                return formulaContext.Disqualified(argument, argumentValue, constraint.Comparison, constraint.Value);
            }
        }

        foreach (var operation in formula.Operations)
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
                case "formula":
                    value = Execute(executionContext, new(operation.InputParameter), logger).Result;
                    break;
                case "result":
                    value = formulaContext.Result;
                    break;
            }

            if (logger.LogLevel == ContextLogLevel.Verbose)
            {
                logger.LogVerbose($"[operation] {formulaContext.Result} {operation.GetOperator()} {value}");
            }

            operation.Perform(formulaContext, value);
        }

        return formulaContext;
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

    private Formula GetFormula(Guid id)
    {
        lock (_lock)
        {
            if (!_formulas.TryGetValue(id, out var formula))
            {
                throw new InvalidOperationException($"There is no formula with id '{id}'.");
            }

            return formula;
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
