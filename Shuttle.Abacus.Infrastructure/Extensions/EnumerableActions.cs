using System.Collections.Generic;

namespace Shuttle.Abacus.Infrastructure
{
    public class EnumerableActions<T> : IEnumerableActions<T>
    {
        private readonly IEnumerable<T> itemsToActOn;


        public EnumerableActions(IEnumerable<T> itemsToActOn)
        {
            this.itemsToActOn = itemsToActOn;
        }

        public void VisitAllItemsUsing(IVisitor<T> visitor)
        {
            foreach (var t in itemsToActOn)
            {
                visitor.Visit(t);
            }
        }

        public Result GetResultOfVisitingAllItemsWith<Result>(IValueReturningVisitor<Result, T> visitor)
        {
            VisitAllItemsUsing(visitor);
            return visitor.GetResult();
        }

        public IEnumerable<T> AllMatching(Specification<T> specification)
        {
            foreach (var t in itemsToActOn)
            {
                if (specification.IsSatisfiedBy(t)) yield return t;
            }
        }

        public IEnumerable<Output> MapAllUsing<Output>(IMapper<T, Output> mapper)
        {
            IList<Output> list = new List<Output>();

            foreach (var t in itemsToActOn)
            {
                list.Add(mapper.MapFrom(t));
            }

            return list;
        }
    }
}
