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
