using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class FormulaCalculationContext : IFormulaCalculationContext
    {
        public IGraphNode GraphNode { get; private set; }

        private readonly IMethodContext methodContext;

        public FormulaCalculationContext(IMethodContext methodContext)
        {
            this.methodContext = methodContext;
            methodContext.IncreaseIndent();

            FormulaTotal = 0;
        }

        public decimal FormulaTotal { get; private set; }

        public void ZeroFormulaTotal()
        {
            FormulaTotal = 0;
        }

        public void SetFormulaTotal(decimal value)
        {
            FormulaTotal = value;

            if (methodContext.LoggerEnabled)
            {
                methodContext.Log("==\t{0}", FormulaTotal.ToString(Resources.FormatDecimal));
            }
        }

        public void Dispose()
        {
            methodContext.DecreaseIndent();
        }

        public ICalculationContext AssignGraphNode(IGraphNode item)
        {
            Guard.AgainstNull(item, "item");

            GraphNode = item;

            return this;
        }

        public void PopulateGraphNode(decimal total, decimal subTotal)
        {
            GraphNode.Populate(total, subTotal);
        }
    }
}
