using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class LimitRepository : Repository<Limit>, ILimitRepository
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly ILimitQueryFactory _limitQueryFactory;
        private readonly IDataRepository<Limit> _repository;

        public LimitRepository(IDatabaseGateway databaseGateway, ILimitQueryFactory limitQueryFactory, IDataRepository<Limit> repository)
        {
            _repository = repository;
            _databaseGateway = databaseGateway;
            _limitQueryFactory = limitQueryFactory;
        }

        public override void Add(Limit item)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Guid id)
        {
            _databaseGateway.ExecuteUsing(_limitQueryFactory.Remove(id));
        }

        public override Limit Get(Guid id)
        {
            var result = _repository.FetchItemUsing(_limitQueryFactory.Get(id));

            if (result == null)
            {
                throw Exceptions.MissingEntity("Limit", id);
            }

            return result;
        }

        public void Add(ILimitOwner owner, Limit limit)
        {
            _databaseGateway.ExecuteUsing(_limitQueryFactory.Add(owner, limit));
        }

        public void Save(Limit limit)
        {
            _databaseGateway.ExecuteUsing(_limitQueryFactory.Save(limit));
        }

        public IEnumerable<Limit> AllForOwner(Guid ownerId)
        {
            return _repository.FetchAllUsing(_limitQueryFactory.AllForOwner(ownerId));
        }
    }
}