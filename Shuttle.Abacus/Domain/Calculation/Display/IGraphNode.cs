using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IGraphNode : IGraphNodeContainer
    {
        string Name { get; }
        decimal Total { get; }
        decimal SubTotal { get; }

        IEnumerable<GraphNodeArgument> GraphNodeArguments { get; }

        void Populate(decimal total, decimal subTotal);
        
        void AddGraphNodeArguments(IEnumerable<GraphNodeArgument> enumerable);
    }
}
