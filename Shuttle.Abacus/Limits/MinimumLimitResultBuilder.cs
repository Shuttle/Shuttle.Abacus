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
    public class MinimumLimitResultBuilder : LimitResultBuilder
    {
        private readonly Calculation calculation;
        private readonly ICalculationResult value;

        public MinimumLimitResultBuilder(Calculation calculation, ICalculationResult value)
        {
            this.calculation = calculation;
            this.value = value;
        }

        public override void Using(IMethodContext methodContext)
        {
            var result = calculation.Execute(methodContext, calculation.CalculationContext(methodContext));

            if (!methodContext.OK)
            {
                return;
            }

            var applyLimit = value.Value < result.Value;

            if (methodContext.LoggerEnabled)
            {
                methodContext.Log("Minimum limit: {0} ({1})", result.Value.ToString(Resources.FormatDecimal),
                            applyLimit
                                ? "limit applied"
                                : "above limit - ok");
            }

            if (applyLimit)
            {
                value.Limit(result.Value);
            }
        }
    }
}
