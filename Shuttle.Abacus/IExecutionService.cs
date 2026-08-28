namespace Shuttle.Abacus;

public interface IExecutionService
{
    IExecutionService Flush();
    IExecutionService AddMatrix(Matrix matrix);
    IExecutionService AddArgument(Argument argument);
    IExecutionService AddFormula(Formula formula);
    Task<ExecutionContext> ExecuteAsync(Guid formulaId, IEnumerable<ArgumentValue> argumentValues, IContextLogger logger, CancellationToken cancellationToken = default);
}
