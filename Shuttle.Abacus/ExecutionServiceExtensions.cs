using Shuttle.Contract;

namespace Shuttle.Abacus;

public static class ExecutionServiceExtensions
{
    public static IExecutionService AddFormulaRange(this IExecutionService service, IEnumerable<Formula>? formulas)
    {
        Guard.AgainstNull(service);

        if (formulas != null)
        {
            foreach (var formula in formulas)
            {
                service.AddFormula(formula);
            }
        }

        return service;
    }

    public static IExecutionService AddArgumentRange(this IExecutionService service, IEnumerable<Argument>? arguments)
    {
        Guard.AgainstNull(service);

        if (arguments != null)
        {
            foreach (var argument in arguments)
            {
                service.AddArgument(argument);
            }
        }

        return service;
    }

    public static IExecutionService AddMatrixRange(this IExecutionService service, IEnumerable<Matrix>? matrices)
    {
        Guard.AgainstNull(service);

        if (matrices != null)
        {
            foreach (var matrix in matrices)
            {
                service.AddMatrix(matrix);
            }
        }

        return service;
    }
}
