using System.Collections.Generic;
using Shuttle.Core.Contract;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public class MatrixRepository : IMatrixRepository
    {
        private readonly IMatrixQuery _query;
        private readonly IEventStore _store;

        public MatrixRepository(IMatrixQuery query, IEventStore store)
        {
            Guard.AgainstNull(query, "query");
            Guard.AgainstNull(store, "store");

            _query = query;
            _store = store;
        }

        public IEnumerable<Matrix> All()
        {
            var result = new List<Matrix>();

            foreach (var row in _query.All())
            {
                var id = MatrixColumns.Id.MapFrom(row);

                var matrix = new Matrix(id);
                var stream = _store.Get(id);

                stream.Apply(matrix);

                result.Add(matrix);
            }

            return result;
        }
    }
}