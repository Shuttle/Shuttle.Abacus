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
