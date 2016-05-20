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
    public class CalculationTotalValueSource : IValueSource
    {
        public static CalculationTotalValueSource Instance = new CalculationTotalValueSource();

        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            return methodContext.Total.Value;
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            return string.Format("{0} (current running total)", operand.ToString(Resources.FormatDecimal));
        }

        public string Name
        {
            get { return "CalculationTotal"; }
        }

        public object Text
        {
            get { return string.Empty; }
        }

        public IValueSource Copy()
        {
            return new CalculationTotalValueSource();
        }
    }
}
