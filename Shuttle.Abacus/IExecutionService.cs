using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IExecutionService
    {
        IExecutionService Flush();
        IExecutionService AddMatrix(Matrix matrix);
        IExecutionService AddArgument(Argument argument);
        IExecutionService AddFormula(Formula formula);
        ExecutionContext Execute(string formulaName, IEnumerable<ArgumentValue> values, IContextLogger logger);
    }
}