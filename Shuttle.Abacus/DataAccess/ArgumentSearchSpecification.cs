namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentSearchSpecification
    {
        public string Name { get; set; }

        public ArgumentSearchSpecification WithName(string name)
        {
            Name = name;

            return this;
        }
    }
}