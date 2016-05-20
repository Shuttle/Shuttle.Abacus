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
