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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
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