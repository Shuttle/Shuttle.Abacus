using Shuttle.Abacus.DataAccess;

namespace Shuttle.Abacus.WebApi
{
    public class TestSearchModel
    {
        public string Name { get; set; }

        public TestSearchSpecification Specification()
        {
            return new TestSearchSpecification()
                .WithName(Name);
        }
    }
}