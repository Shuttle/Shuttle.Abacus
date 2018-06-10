using System.Collections.Generic;
using Shuttle.Core.Contract;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentRepository : IArgumentRepository
    {
        private readonly IArgumentQuery _query;
        private readonly IEventStore _store;

        public ArgumentRepository(IArgumentQuery query, IEventStore store)
        {
            Guard.AgainstNull(query, nameof(query));
            Guard.AgainstNull(store, nameof(store));

            _query = query;
            _store = store;
        }

        public IEnumerable<Argument> All()
        {
            var result = new List<Argument>();

            foreach (var row in _query.Search(new ArgumentSearchSpecification()))
            {
                var id = Columns.Id.MapFrom(row);

                var argument = new Argument(id);
                var stream = _store.Get(id);

                stream.Apply(argument);

                result.Add(argument);
            }

            return result;
        }
    }
}