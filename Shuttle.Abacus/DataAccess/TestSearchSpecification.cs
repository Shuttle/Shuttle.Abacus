namespace Shuttle.Abacus.DataAccess
{
    public class TestSearchSpecification
    {
        public string Name { get; private set; }

        public TestSearchSpecification WithName(string name)
        {
            Name = name;

            return this;
        }
    }
}