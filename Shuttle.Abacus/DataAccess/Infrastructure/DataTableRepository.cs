using System.Collections.Generic;
using System.Linq;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class DataTableRepository<T> : IDataTableRepository<T>
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IDataTableMapper<T> _mapper;

        public DataTableRepository(IDatabaseGateway databaseGateway, IDataTableMapper<T> mapper)
        {
            _databaseGateway = databaseGateway;
            _mapper = mapper;
        }

        public IEnumerable<T> FetchAllUsing(IQuery query)
        {
            return _mapper.MapFrom(_databaseGateway.GetDataTableFor(query));
        }

        public T FetchItemUsing(IQuery query)
        {
            return FetchAllUsing(query).First();
        }

        public bool Contains(IQuery query)
        {
            return _databaseGateway.GetScalarUsing<int>(query) == 1;
        }
    }
}