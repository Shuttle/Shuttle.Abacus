namespace Shuttle.Abacus.DataAccess
{
    public class MatrixSearchSpecification
    {
        public string Name { get; private set; }

        public MatrixSearchSpecification MatchingName(string name)
        {
            Name = name;

            return this;
        }
    }
}