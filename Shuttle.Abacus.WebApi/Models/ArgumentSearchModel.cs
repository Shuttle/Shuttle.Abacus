using Shuttle.Abacus.DataAccess;

namespace Shuttle.Abacus.WebApi
{
    public class ArgumentSearchModel
    {
        public string Name { get; set; }

        public ArgumentSearchSpecification Specification()
        {
            return new ArgumentSearchSpecification()
                .WithName(Name);
        }
    }
}