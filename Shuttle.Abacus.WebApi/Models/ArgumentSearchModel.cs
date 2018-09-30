using System;
using Shuttle.Abacus.DataAccess;

namespace Shuttle.Abacus.WebApi
{
    public class ArgumentSearchModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ArgumentSearchSpecification Specification()
        {
            var specification = new ArgumentSearchSpecification();

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