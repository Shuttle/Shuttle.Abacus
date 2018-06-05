using Shuttle.Abacus.DataAccess;

namespace Shuttle.Abacus.WebApi
{
    public class FormulaSearchModel
    {
        public string Name { get; set; }
        public string MaximumFormulaName { get; set; }
        public string MinimumFormulaName { get; set; }

        public FormulaSearchSpecification Specification()
        {
            return new FormulaSearchSpecification()
                .WithName(Name)
                .WithMaximumFormulaName(MaximumFormulaName)
                .WithMinimumFormulaName(MinimumFormulaName);
        }
    }
}