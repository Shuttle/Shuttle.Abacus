namespace Shuttle.Abacus.DataAccess
{
    public class FormulaSearchSpecification
    {
        public string Name { get; private set; }
        public string MaximumFormulaName { get; private set; }
        public string MinimumFormulaName { get; private set; }

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