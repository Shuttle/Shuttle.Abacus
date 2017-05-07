using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.Domain
{
    public class MaximumLimitResultBuilder 
    {
        private readonly Formula _formula;
        private readonly ICalculationResult value;

        public MaximumLimitResultBuilder(Formula formula, ICalculationResult value)
        {
            _formula = formula;
            this.value = value;
        }

        public void Using(IMethodContext methodContext)
        {
            //TODO
            //var result = _formula.Execute(methodContext, _formula.CalculationContext(methodContext));

            //if (!methodContext.OK)
            //{
            //    return;
            //}

            //var applyLimit = value.Value > result.Value;

            //if (methodContext.LoggerEnabled)
            //{
            //    methodContext.Log("Maximum limit: {0} ({1})", result.Value.ToString(Resources.FormatDecimal),
            //        applyLimit
            //            ? "limit applied"
            //            : "below limit - ok");
            //}

            //if (applyLimit)
            //{
            //    value.Limit(result.Value);
            //}
        }
    }
}