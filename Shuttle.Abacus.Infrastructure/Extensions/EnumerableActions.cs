using System.Collections.Generic;

namespace Shuttle.Abacus.Infrastructure
{
    public class EnumerableActions<T>
    {
        private readonly IEnumerable<T> itemsToActOn;

        public EnumerableActions(IEnumerable<T> itemsToActOn)
        {
            this.itemsToActOn = itemsToActOn;
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
