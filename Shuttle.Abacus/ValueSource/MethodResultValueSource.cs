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

namespace Shuttle.Abacus
{
    public class MethodResultValueSource : IValueSource, IValueSelectionHolder
    {
        private readonly Method method;

        public MethodResultValueSource(Method method)
        {
            this.method = method;
        }

        public string ValueSelection
        {
            get { return method.Id.ToString("n"); }
        }

        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            using (var wrapped = methodContext.Wrapped(method.MethodName).AssignGraphNode(calculationContext.GraphNode.AddGraphNode(method.MethodName)))
            {
                if (wrapped.LoggerEnabled)
                {
                    wrapped.Log("Starting Method Calculation: {0}", method.MethodName);
                    wrapped.Log();
                }

                method.Calculate(wrapped);

                if (!wrapped.OK)
                {
                    foreach (var errorMessage in wrapped.ErrorMessages)
                    {
                        methodContext.AddErrorMessage(errorMessage);
                    }

                    return 0;
                }

                return wrapped.Total.Value;
            }
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            return string.Format("{0} (from method result '{1}')", operand.ToString(Resources.FormatDecimal), method.MethodName);
        }

        public string Name
        {
            get { return "MethodResult"; }
        }

        public object Text
        {
            get { return method.MethodName; }
        }

        public IValueSource Copy()
        {
            return new MethodResultValueSource(method);
        }
    }
}
