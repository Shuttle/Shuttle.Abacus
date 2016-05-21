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

using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus
{
    public class FormulaTotalValueSource : IValueSource
    {
        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            return ((IFormulaCalculationContext)calculationContext).FormulaTotal;
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            return string.Format("{0} (formula total)", operand.ToString(Resources.FormatDecimal));
        }

        public string Name
        {
            get { return "FormulaTotal"; }
        }

        public object Text
        {
            get { return string.Empty; }
        }

        public IValueSource Copy()
        {
            return new FormulaTotalValueSource();
        }
    }
}
