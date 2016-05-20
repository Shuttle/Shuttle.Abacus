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
    public class DecimalTableValueSource :
        IValueSource,
        IValueSelectionHolder,
        ISpecification<IMethodContext>
    {
        private readonly DecimalTable decimalTable;

        public DecimalTableValueSource(DecimalTable decimalTable)
        {
            this.decimalTable = decimalTable;
        }

        public bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            return decimalTable.IsSatisfiedBy(collectionMethodContext);
        }

        public string ValueSelection
        {
            get { return decimalTable.Id.ToString("n"); }
        }

        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            return decimalTable.Get(methodContext).Value;
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            var rate = decimalTable.Find(methodContext);

            return rate != null
                       ? string.Format("{0} (from rate table '{1}' - {2})", rate.Value, decimalTable.Name,
                                       rate.Description(methodContext))
                       : string.Empty;
        }

        public string Name
        {
            get { return "DecimalTable"; }
        }

        public object Text
        {
            get { return decimalTable.Name; }
        }

        public IValueSource Copy()
        {
            return new DecimalTableValueSource(decimalTable);
        }
    }
}
