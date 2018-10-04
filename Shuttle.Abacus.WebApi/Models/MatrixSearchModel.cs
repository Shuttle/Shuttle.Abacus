using System;
using Shuttle.Abacus.DataAccess;

namespace Shuttle.Abacus.WebApi
{
    public class MatrixSearchModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public MatrixSearchSpecification Specification()
        {
            var specification = new MatrixSearchSpecification();

            if (!string.IsNullOrWhiteSpace(Name))
            {
                specification.MatchingName(Name);
            }

            if (!Id.Equals(Guid.Empty))
            {
                specification.WithId(Id);
            }

            return specification;
        }
    }
}