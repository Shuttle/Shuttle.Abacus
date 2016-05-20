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
using System.Collections;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class FormulaCollection : IEnumerable<Formula>
    {
        private readonly List<Formula> formulas = new List<Formula>();

        public IEnumerator<Formula> GetEnumerator()
        {
            return formulas.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<Guid> RequiredCalculationIds()
        {
            var result = new List<Guid>();

            foreach (var formula in formulas)
            {
                foreach (var requiredCalculationId in formula.RequiredCalculationIds())
                {
                    if (!result.Contains(requiredCalculationId))
                    {
                        result.Add(requiredCalculationId);
                    }
                }
            }

            return result;
        }

        public void Add(Formula formula)
        {
            formulas.Add(formula);
        }

        public Formula Find(IMethodContext collectionContext)
        {
            return formulas.Find(formula => formula.IsSatisfiedBy(collectionContext));
        }

        public Formula Get(Guid id)
        {
            return formulas.Find(formula => formula.Id.Equals(id));
        }

        public Formula Remove(Guid id)
        {
            var formula = Get(id);

            formulas.Remove(formula);

            return formula;
        }

        public FormulaCollection Copy()
        {
            var result = new FormulaCollection();

            formulas.ForEach(formula => result.Add(formula.Copy()));

            return result;
        }
    }
}
