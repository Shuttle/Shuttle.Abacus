using System.Collections.Generic;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public static class ExecutionServiceExtensions
    {
        public static IExecutionService AddFormulaRange(this IExecutionService service, IEnumerable<Formula> formulas)
        {
            Guard.AgainstNull(service, nameof(service));

            if (formulas != null)
            {
                foreach (var formula in formulas)
                {
                    service.AddFormula(formula);
                }
            }

            return service;
        }

        public static IExecutionService AddArgumentRange(this IExecutionService service,
            IEnumerable<Argument> arguments)
        {
            Guard.AgainstNull(service, nameof(service));

            if (arguments != null)
            {
                foreach (var argument in arguments)
                {
                    service.AddArgument(argument);
                }
            }

            return service;
        }

        public static IExecutionService AddMatrixRange(this IExecutionService service, IEnumerable<Matrix> matrices)
        {
            Guard.AgainstNull(service, nameof(service));

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
}