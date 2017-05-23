using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Infrastructure;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaRepository : IFormulaRepository
    {
        private readonly IFormulaQuery _query;
        private readonly IEventStore _store;

        public FormulaRepository(IFormulaQuery query, IEventStore store)
        {
            Guard.AgainstNull(query, "query");
            Guard.AgainstNull(store, "store");

            _query = query;
            _store = store;
        }

        public IEnumerable<Formula> All()
        {
            var result = new List<Formula>();

            foreach (var row in _query.All())
            {
                var id = FormulaColumns.Id.MapFrom(row);

                var argument = new Formula(id);
                var stream = _store.Get(id);

                stream.Apply(argument);

                result.Add(argument);
            }

            return result;
        }
    }
}