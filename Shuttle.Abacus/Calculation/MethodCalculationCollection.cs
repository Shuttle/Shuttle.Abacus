using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class MethodCalculationCollection : CalculationCollection
    {
        public MethodCalculationCollection()
            : base((string) "method")
        {
        }

        public override ICalculationResult Execute(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            Guard.AgainstNull(methodContext, "methodContext");

            if (methodContext.LoggerEnabled)
            {
                methodContext.Log("Starting calculation: '{0}'", Name);
            }

            foreach (var calculation in calculations)
            {
                using (var context = calculation.CalculationContext(methodContext).AssignGraphNode(methodContext.GraphNode.AddGraphNode(calculation.Name)))
                {
                    context.GraphNode.AddGraphNodeArguments(calculation.GraphNodeArguments);

                    var calculationResult = (AbstractCalculationResult)calculation.Execute(methodContext, context);
                    var subTotalCalculationResult = methodContext.GetSubTotal(calculation.Name);

                    context.PopulateGraphNode(calculationResult.Value, subTotalCalculationResult.Value);

                    if (!methodContext.OK)
                    {
                        return new CalculationCollectionResult(calculation);
                    }

                    if (!methodContext.LoggerEnabled)
                    {
                        continue;
                    }

                    methodContext.Log(methodContext.Total.Description());
                    methodContext.Log();
                }
            }

            ApplyLimits(methodContext, methodContext.Total);

            if (methodContext.LoggerEnabled)
            {
                methodContext.Log("Total for method: {0}", methodContext.Total.Description());
                methodContext.Log();
            }

            methodContext.GraphNode.Populate(methodContext.Total.Value, methodContext.Total.Value);

            return methodContext.Total;
        }
    }
}
