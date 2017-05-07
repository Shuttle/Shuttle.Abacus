using System;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.Domain
{
    public class MinimumLimitResultBuilder 
    {
        private readonly Formula _formula;
        private readonly ICalculationResult value;

        public MinimumLimitResultBuilder(Formula formula, ICalculationResult value)
        {
            this._formula = formula;
            this.value = value;
        }

        public void Using(IMethodContext methodContext)
        {
            throw new NotImplementedException();
            //var result = _formula.Execute(methodContext, _formula.CalculationContext(methodContext));

            //if (!methodContext.OK)
            //{
            //    return;
            //}

            //var applyLimit = value.Value < result.Value;

            //if (methodContext.LoggerEnabled)
            //{
            //    methodContext.Log("Minimum limit: {0} ({1})", result.Value.ToString(Resources.FormatDecimal),
            //                applyLimit
            //                    ? "limit applied"
            //                    : "above limit - ok");
            //}

            //if (applyLimit)
            //{
            //    value.Limit(result.Value);
            //}
        }
    }
}
