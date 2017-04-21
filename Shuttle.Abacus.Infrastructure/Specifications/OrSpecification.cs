using System.Collections.Generic;

namespace Shuttle.Abacus.Infrastructure
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        public OrSpecification(IEnumerable<Specification<T>> specifications)
            : base(specifications)
        {
        }

        public OrSpecification(
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
                if (specification.IsSatisfiedBy(item))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
