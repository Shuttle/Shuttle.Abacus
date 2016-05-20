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

using System;

namespace Shuttle.Abacus
{
    public class CalculationSubTotalValueSource : IValueSource, ICalculationValueSource
    {
        private readonly Calculation calculation;
        private Guid calculationId;

        public CalculationSubTotalValueSource(Calculation calculation)
        {
            this.calculation = calculation;

            calculationId = calculation.Id;
        }

        public string ValueSelection
        {
            get { return calculationId.ToString("n"); }
        }

        public void AssignCalculationId(Guid id)
        {
            calculationId = id;
        }

        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            return methodContext.GetSubTotal(calculation.Name).Value;
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            return string.Format("{0} (from sub-total '{1}')", operand.ToString(Resources.FormatDecimal), calculation.Name);
        }

        public string Name
        {
            get { return "CalculationSubTotal"; }
        }

        public object Text
        {
            get { return calculation.Name; }
        }

        public IValueSource Copy()
        {
            return new CalculationSubTotalValueSource(calculation);
        }
    }
}
