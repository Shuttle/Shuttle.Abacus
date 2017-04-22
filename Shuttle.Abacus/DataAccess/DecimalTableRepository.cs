using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class DecimalTableRepository : Repository<DecimalTable>, IDecimalTableRepository
    {
        private readonly ICache cache;
        private readonly IDatabaseGateway gateway;
        private readonly IDataRepository<DecimalTable> repository;

        public DecimalTableRepository(IDataRepository<DecimalTable> repository, IDatabaseGateway gateway, ICache cache)
        {
            this.repository = repository;
            this.gateway = gateway;
            this.cache = cache;
        }

        public override void Add(DecimalTable item)
        {
            gateway.ExecuteUsing(DecimalTableTableAccess.Add(item));
        }

        public override void Remove(DecimalTable item)
        {
            gateway.ExecuteUsing(DecimalTableTableAccess.Remove(item));
        }

        public override DecimalTable Get(Guid id)
        {
            var key = string.Format("DecimalTable|{0}", id);

            var result = cache.Get<DecimalTable>(key);

            if (result != null)
            {
                return result;
            }

            result = repository.FetchItemUsing(DecimalTableTableAccess.Get(id));

            Guard.AgainstMissing<DecimalValueQueryFactory>(result, id);

            cache.Add(key, result);

            return result;
        }

        public IEnumerable<DecimalTable> All()
        {
            return repository.FetchAllUsing(DecimalTableTableAccess.All());
        }

        public void Save(DecimalTable decimalTable)
        {
            gateway.ExecuteUsing(DecimalTableTableAccess.Save(decimalTable));
        }
    }
}