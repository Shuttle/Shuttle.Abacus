using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Infrastructure;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaRepository : IFormulaRepository
    {
        private readonly IFormulaQuery _query;
        private readonly IEventStore _eventStore;

        public FormulaRepository(IFormulaQuery query, IEventStore eventStore)
        {
            Guard.AgainstNull(query, "query");
            Guard.AgainstNull(eventStore, "eventStore");

            _query = query;
            _eventStore = eventStore;
        }

        public IEnumerable<Formula> All()
        {
            var result = new List<Formula>();

            foreach (var row in _query.All())
            {
                var id = FormulaColumns.FormulaId.MapFrom(row);

                var formula = new Formula(id);
                var stream = _eventStore.Get(id);

                stream.Apply(formula);

                result.Add(formula);
            }

            return result;
        }
    }
}