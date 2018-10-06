using System;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaSearchSpecification
    {
        public Guid? Id { get; private set; }
        public string Name { get; private set; }
        public string MaximumFormulaName { get; private set; }
        public string MinimumFormulaName { get; private set; }

        public FormulaSearchSpecification WithId(Guid id)
        {
            Id = id;

            return this;
        }

        public FormulaSearchSpecification WithName(string name)
        {
            Name = name;

            return this;
        }

        public FormulaSearchSpecification WithMaximumFormulaName(string name)
        {
            MaximumFormulaName = name;

            return this;
        }

        public FormulaSearchSpecification WithMinimumFormulaName(string name)
        {
            MinimumFormulaName = name;

            return this;
        }
    }
}