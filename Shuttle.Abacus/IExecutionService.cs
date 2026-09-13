namespace Shuttle.Abacus;

public interface IExecutionService
{
    IExecutionService Flush();
    IExecutionService AddMatrix(Matrix matrix);
    IExecutionService AddArgument(Argument argument);
    IExecutionService AddAlgorithm(Algorithm algorithm);
    Task<ExecutionContext> ExecuteAsync(Guid algorithmId, IEnumerable<ArgumentValue> argumentValues, IContextLogger logger, CancellationToken cancellationToken = default);
}
