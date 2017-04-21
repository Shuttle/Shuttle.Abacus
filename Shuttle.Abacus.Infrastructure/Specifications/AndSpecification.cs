using System.Collections.Generic;

namespace Shuttle.Abacus.Infrastructure
{
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        public AndSpecification(IEnumerable<Specification<T>> specifications)
            : base(specifications)
        {
        }

        public AndSpecification(
            Specification<T> augmentSpecification,
            Specification<T> withSpecification)
            : base(new List<Specification<T>>
                       {
                           augmentSpecification,
                           withSpecification
                       })
        {
        }

        public override bool IsSatisfiedBy(T item)
        {
            foreach (var specification in specifications)
            {
                if (!specification.IsSatisfiedBy(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
