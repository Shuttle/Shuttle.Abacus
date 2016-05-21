using System.Collections;
using System.Collections.Generic;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class GraphNodeCollection : IEnumerable<IGraphNode>
    {
        private readonly List<IGraphNode> items;

        private GraphNodeCollection(IEnumerable<IGraphNode> enumerable)
        {
            items = new List<IGraphNode>(enumerable);
        }

        public GraphNodeCollection()
        {
            items = new List<IGraphNode>();
        }

        public GraphNodeCollection Items
        {
            get { return new GraphNodeCollection(items); }
        }

        public IEnumerator<IGraphNode> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IGraphNode Add(string name)
        {
            var result = new GraphNode(name);

            items.Add(result);

            return result;
        }

        public void Add(IGraphNode item)
        {
            Guard.AgainstNull(item, "item");

            items.Add(item);
        }
    }
}
