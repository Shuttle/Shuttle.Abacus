using System.Collections.Generic;

namespace Shuttle.Abacus.Infrastructure
{
    public class NotSpecification<T> : CompositeSpecification<T>
    {
        public NotSpecification(Specification<T> specification)
            : base(new List<Specification<T>>
                       {
                           specification
                       })
        {
        }

        public NotSpecification(IEnumerable<Specification<T>> specifications)
            : base(specifications)
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
