using System;
using System.Collections.Generic;
using Abacus.Domain;
using Abacus.Infrastructure;

namespace Abacus.Data
{
    public class MethodRepository : Repository<Method>, IMethodRepository
    {
        private readonly IDataRowRepository<Method> repository;
        private readonly IDatabaseGateway gateway;
        private readonly ICache cache;

        public MethodRepository(IDataRowRepository<Method> repository, IDatabaseGateway gateway, ICache cache)
        {
            this.repository = repository;
            this.gateway = gateway;
            this.cache = cache;
        }

        public override void Add(Method item)
        {
            gateway.ExecuteUsing(MethodTableAccess.Add(item));
        }

        public override void Remove(Method item)
        {
            gateway.ExecuteUsing(MethodTableAccess.Remove(item));
        }

        public override Method Get(Guid id)
        {
            var key = string.Format("Method|{0}", id);

            var result = cache.Get<Method>(key);

            if (result != null)
            {
                return result;
            }

            result = repository.FetchItemUsing(MethodTableAccess.Get(id));

            Guard.AgainstMissing<Method>(result, id);

            cache.Add(key, result);

            return result;
        }

        public void Save(Method item)
        {
            gateway.ExecuteUsing(MethodTableAccess.Save(item));
        }

        public Method Get(string methodName)
        {
            var key = string.Format("Name|{0}", methodName);

            var result = cache.Get<Method>(key);

            if (result != null)
            {
                return result;
            }

            result = repository.FetchItemUsing(MethodTableAccess.Get(methodName));

            Guard.AgainstMissing<Method>(result, methodName);

            cache.Add(key, result);

            return result;
        }
    }
}
