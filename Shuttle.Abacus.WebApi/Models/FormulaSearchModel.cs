using System;
using Shuttle.Abacus.DataAccess;

namespace Shuttle.Abacus.WebApi
{
    public class FormulaSearchModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MaximumFormulaName { get; set; }
        public string MinimumFormulaName { get; set; }

        public FormulaSearchSpecification Specification()
        {
            var specification = new FormulaSearchSpecification();

            if (!Id.Equals(Guid.Empty))
            {
                specification.WithId(Id);
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                specification.WithName(Name);
            }

            if (!string.IsNullOrWhiteSpace(MaximumFormulaName))
            {
                specification.WithMaximumFormulaName(MaximumFormulaName);
            }

            if (!string.IsNullOrWhiteSpace(MinimumFormulaName))
            {
                specification.WithMinimumFormulaName(MinimumFormulaName);
            }

            return specification;
        }
    }
}