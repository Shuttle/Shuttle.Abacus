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
    public abstract class FormulaOperation :
        ISpecification<IMethodContext>
    {
        protected FormulaOperation(IValueSource source)
        {
            Guard.AgainstNull(source, "source");

            ValueSource = source;
        }

        public IValueSource ValueSource { get; private set; }

        public abstract string Symbol { get; }
        public abstract string Name { get; }

        public bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            var specification = ValueSource as ISpecification<IMethodContext>;

            return specification == null || specification.IsSatisfiedBy(collectionMethodContext);
        }

        public abstract decimal Execute(decimal total, decimal operand);
        public abstract decimal Operand(IMethodContext methodContext, IFormulaCalculationContext calculationContext);

        public abstract FormulaOperation Copy();
    }
}
