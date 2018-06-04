using System.Collections.Generic;
using Shuttle.Core.Contract;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaRepository : IFormulaRepository
    {
        private readonly IEventStore _eventStore;
        private readonly IFormulaQuery _query;

        public FormulaRepository(IFormulaQuery query, IEventStore eventStore)
        {
            Guard.AgainstNull(query, nameof(query));
            Guard.AgainstNull(eventStore, nameof(eventStore));

            _query = query;
            _eventStore = eventStore;
        }

        public IEnumerable<Formula> All()
        {
            var result = new List<Formula>();

            foreach (var row in _query.Search(new FormulaSearchSpecification()))
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