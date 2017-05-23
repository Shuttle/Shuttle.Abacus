using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Caching;
using System.Threading;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Recall;

namespace Shuttle.Abacus
{
    public class ExecutionTask : IExecutionTask
    {
        private readonly object _lock = new object();
        private bool _cached;

        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IEventStore _eventStore;
        private readonly IFormulaQuery _formulaQuery;
        private readonly IArgumentQuery _argumentQuery;

        private readonly CacheItemPolicy _policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.MaxValue };
        private MemoryCache _cache;

        public ExecutionTask(IDatabaseContextFactory databaseContextFactory, IEventStore eventStore, 
            IFormulaQuery formulaQuery, IArgumentQuery argumentQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(eventStore, "eventStore");
            Guard.AgainstNull(formulaQuery, "formulaQuery");
            Guard.AgainstNull(argumentQuery, "argumentQuery");

            _databaseContextFactory = databaseContextFactory;
            _eventStore = eventStore;
            _formulaQuery = formulaQuery;
            _argumentQuery = argumentQuery;
        }

        public ExecutionContext Execute(string formulaName, IEnumerable<ArgumentValue> values)
        {
            throw new NotImplementedException();
            //if (!_cached)
            //{
            //    Cache();
            //}

            //var formula = Get<Formula>(formulaName);
        }

        public void Flush()
        {
            lock (_lock)
            {
                if (!_cached)
                {
                    return;
                }

                _cache.Dispose();

                _cached = false;
            }
        }

        private T Get<T>(string key)
        {
            var result = _cache[string.Format("[{0}]:{1}", typeof(T).Name, key)];

            if (result == null)
            {
                throw new ApplicationException();
            }

            return (T)result;
        }

        private void Cache()
        {
            lock (_lock)
            {
                if (_cached)
                {
                    return;
                }

                _cache = new MemoryCache("execution");

                using (_databaseContextFactory.Create())
                {
                    foreach (var row in _formulaQuery.All())
                    {
                        Cache<Formula>(FormulaColumns.Id.MapFrom(row), formula => formula.Name);
                    }
                }

                using (_databaseContextFactory.Create())
                {
                    foreach (var row in _argumentQuery.All())
                    {
                        Cache<Argument>(ArgumentColumns.Id.MapFrom(row), argument => argument.Name);
                    }
                }

                _cached = true;
            }
        }

        private void Cache<T>(Guid id, Func<T, string> keyCallback)
        {
            var type = typeof(T);

            var instance = Activator.CreateInstance(type, id);

            var stream = _eventStore.Get(id);
            stream.Apply(instance);

            _cache.Add(new CacheItem(string.Format("[{0}]:{1}", type.Name, keyCallback.Invoke((T)instance)), instance), _policy);
        }
    }
}