using Shuttle.Abacus.DataAccess;

namespace Shuttle.Abacus.WebApi
{
    public class MatrixSearchModel
    {
        public string Name { get; set; }

        public MatrixSearchSpecification Specification()
        {
            return new MatrixSearchSpecification()
                .MatchingName(Name);
        }
    }
}