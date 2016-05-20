/*
    This file forms part of Shuttle.Abacus.

    Shuttle.Abacus - A constraint-based calculation engine.
    Copyright (C) 2016  Eben Roux

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

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
