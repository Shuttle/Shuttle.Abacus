namespace Shuttle.Abacus.Domain
{
    public class MaximumLimitResultBuilder : LimitResultBuilder
    {
        private readonly Calculation calculation;
        private readonly ICalculationResult value;

        public MaximumLimitResultBuilder(Calculation calculation, ICalculationResult value)
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

            var applyLimit = value.Value > result.Value ;

            if (methodContext.LoggerEnabled)
            {
                methodContext.Log("Maximum limit: {0} ({1})", result.Value.ToString(Resources.FormatDecimal),
                            applyLimit
                                ? "limit applied"
                                : "below limit - ok");
            }

            if (applyLimit)
            {
                value.Limit(result.Value);
            }
        }
    }
}
