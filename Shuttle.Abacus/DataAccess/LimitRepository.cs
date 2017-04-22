using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess
{
    public class LimitRepository : Repository<Limit>, ILimitRepository
    {
        private readonly IDataRepository<Limit> repository;
        private readonly IDatabaseGateway gateway;

        public LimitRepository(IDataRepository<Limit> repository, IDatabaseGateway gateway)
        {
            this.repository = repository;
            this.gateway = gateway;
        }

        public override void Add(Limit item)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Limit item)
        {
            gateway.ExecuteUsing(LimitTableAccess.Remove(item));
        }

        public override Limit Get(Guid id)
        {
            var result = repository.FetchItemUsing(LimitTableAccess.Get(id));

            Guard.AgainstMissing<Limit>(result, id);

            return result;
        }

        public void Add(ILimitOwner owner, Limit limit)
        {
            gateway.ExecuteUsing(LimitTableAccess.Add(owner, limit));
        }

        public void Save(Limit limit)
        {
            gateway.ExecuteUsing(LimitTableAccess.Save(limit));
        }

        public IEnumerable<Limit> AllForOwner(Guid ownerId)
        {
            return repository.FetchAllUsing(LimitTableAccess.AllForOwner(ownerId));
        }
    }
}
