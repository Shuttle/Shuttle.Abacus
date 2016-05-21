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
