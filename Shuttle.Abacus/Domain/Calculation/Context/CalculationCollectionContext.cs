using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class CalculationCollectionContext : ICalculationCollectionContext
    {
        private readonly IMethodContext methodContext;

        public CalculationCollectionContext(IMethodContext methodContext)
        {
            this.methodContext = methodContext;

            methodContext.IncreaseIndent();
        }

        public void Dispose()
        {
            methodContext.DecreaseIndent();
        }

        public IGraphNode GraphNode { get; private set; }

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
