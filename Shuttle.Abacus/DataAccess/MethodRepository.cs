using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class MethodRepository : Repository<Method>, IMethodRepository
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IMethodQueryFactory _methodQueryFactory;
        private readonly ICache _cache;
        private readonly IDataRepository<Method> _repository;

        public MethodRepository(IDatabaseGateway databaseGateway, IMethodQueryFactory methodQueryFactory,
            IDataRepository<Method> repository, ICache cache)
        {
            this._repository = repository;
            _databaseGateway = databaseGateway;
            _methodQueryFactory = methodQueryFactory;
            this._cache = cache;
        }

        public override void Add(Method item)
        {
            _databaseGateway.ExecuteUsing(_methodQueryFactory.Add(item));
        }

        public override void Remove(Method item)
        {
            _databaseGateway.ExecuteUsing(_methodQueryFactory.Remove(item));
        }

        public override Method Get(Guid id)
        {
            var key = string.Format("Method|{0}", id);

            var result = _cache.Get<Method>(key);

            if (result != null)
            {
                return result;
            }

            result = _repository.FetchItemUsing(_methodQueryFactory.Get(id));

            if (result == null)
            {
                throw Exceptions.MissingEntity("Method", id);
            }

            _cache.Add(key, result);

            return result;
        }

        public void Save(Method item)
        {
            _databaseGateway.ExecuteUsing(_methodQueryFactory.Save(item));
        }

        public Method Get(string methodName)
        {
            var key = string.Format("Name|{0}", methodName);

            var result = _cache.Get<Method>(key);

            if (result != null)
            {
                return result;
            }

            result = _repository.FetchItemUsing(_methodQueryFactory.Get(methodName));

            if (result == null)
            {
                throw new MissingEntityException(string.Format("Could not find entity 'Method' with a name of '{0}'.", methodName));
            }

            _cache.Add(key, result);

            return result;
        }
    }
}