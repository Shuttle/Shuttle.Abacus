using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class GraphNode : IGraphNode
    {
        private readonly GraphNodeCollection items = new GraphNodeCollection();
        private List<GraphNodeArgument> graphNodeArguments = new List<GraphNodeArgument>();

        public GraphNode(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public IEnumerable<GraphNodeArgument> GraphNodeArguments
        {
            get { return new ReadOnlyCollection<GraphNodeArgument>(graphNodeArguments); }
        }

        public void Populate(decimal total, decimal subTotal)
        {
            Total = total;
            SubTotal = subTotal;
        }

        public void AddGraphNodeArguments(IEnumerable<GraphNodeArgument> enumerable)
        {
            graphNodeArguments = new List<GraphNodeArgument>(enumerable);
        }

        public decimal Total { get; private set; }
        public decimal SubTotal { get; private set; }


        public GraphNodeCollection GraphNodes
        {
            get { return items; }
        }

        public void AddGraphNode(IGraphNode item)
        {
            Guard.AgainstNull(item, "item");

            items.Add(item);
        }

        public IGraphNode AddGraphNode(string name)
        {
            return items.Add(name);
        }
    }
}