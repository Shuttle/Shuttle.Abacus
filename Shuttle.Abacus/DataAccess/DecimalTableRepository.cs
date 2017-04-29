using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class DecimalTableRepository : Repository<DecimalTable>, IDecimalTableRepository
    {
        private readonly ICache _cache;
        private readonly IDatabaseGateway _databaseGateway;
        private readonly DecimalTableQueryFactory _decimalTableQueryFactory;
        private readonly IDataRepository<DecimalTable> _repository;

        public DecimalTableRepository(IDatabaseGateway databaseGateway,
            DecimalTableQueryFactory decimalTableQueryFactory, IDataRepository<DecimalTable> repository, ICache cache)
        {
            _repository = repository;
            _databaseGateway = databaseGateway;
            _decimalTableQueryFactory = decimalTableQueryFactory;
            _cache = cache;
        }

        public override void Add(DecimalTable item)
        {
            _databaseGateway.ExecuteUsing(_decimalTableQueryFactory.Add(item));
        }

        public override void Remove(Guid id)
        {
            _databaseGateway.ExecuteUsing(_decimalTableQueryFactory.Remove(id));
        }

        public override DecimalTable Get(Guid id)
        {
            var key = string.Format("DecimalTable|{0}", id);

            var result = _cache.Get<DecimalTable>(key);

            if (result != null)
            {
                return result;
            }

            result = _repository.FetchItemUsing(_decimalTableQueryFactory.Get(id));

            if (result == null)
            {
                throw Exceptions.MissingEntity<DecimalValueQueryFactory>(id);
            }

            _cache.Add(key, result);

            return result;
        }

        public IEnumerable<DecimalTable> All()
        {
            return _repository.FetchAllUsing(_decimalTableQueryFactory.All());
        }

        public void Save(DecimalTable decimalTable)
        {
            _databaseGateway.ExecuteUsing(_decimalTableQueryFactory.Save(decimalTable));
        }
    }
}