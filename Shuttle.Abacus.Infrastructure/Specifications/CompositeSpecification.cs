using System.Collections.Generic;

namespace Shuttle.Abacus.Infrastructure
{
    public abstract class CompositeSpecification<T> : Specification<T>
    {
        protected readonly IEnumerable<Specification<T>> specifications;

        protected CompositeSpecification(IEnumerable<Specification<T>> specifications)
        {
            this.specifications = specifications;
        }

        protected IEnumerable<Specification<T>> Specifications => specifications;
    }
}
