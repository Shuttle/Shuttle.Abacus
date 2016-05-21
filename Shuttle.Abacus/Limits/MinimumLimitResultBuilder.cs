using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus
{
    public class MinimumLimitResultBuilder : LimitResultBuilder
    {
        private readonly Calculation calculation;
        private readonly ICalculationResult value;

        public MinimumLimitResultBuilder(Calculation calculation, ICalculationResult value)
        {
            this.calculation = calculation;
            this.value = value;
        }

        public override void Using(IMethodContext methodContext)
        {
            var result = calculation.Execute(methodContext, calculation.CalculationContext(methodContext));

            if (!methodContext.OK)
            {
                return;
            }

            var applyLimit = value.Value < result.Value;

            if (methodContext.LoggerEnabled)
            {
                methodContext.Log("Minimum limit: {0} ({1})", result.Value.ToString(Resources.FormatDecimal),
                            applyLimit
                                ? "limit applied"
                                : "above limit - ok");
            }

            if (applyLimit)
            {
                value.Limit(result.Value);
            }
        }
    }
}
