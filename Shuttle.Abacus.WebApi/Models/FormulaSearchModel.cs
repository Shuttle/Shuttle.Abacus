using Shuttle.Abacus.DataAccess;

namespace Shuttle.Abacus.WebApi
{
    public class FormulaSearchModel
    {
        public string Name { get; set; }

        public FormulaSearchSpecification Specification()
        {
            return new FormulaSearchSpecification()
                .WithName(Name);
        }
    }
}