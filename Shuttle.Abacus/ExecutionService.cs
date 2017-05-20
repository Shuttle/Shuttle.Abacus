using System;
using System.Collections.Specialized;
using System.Reflection;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Recall;

namespace Shuttle.Abacus
{
    public class ExecutionService : ICalculationService
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IEventStore _eventStore;
        private readonly IKeyStore _keyStore;

        public ExecutionService(IDatabaseGateway databaseGateway, IEventStore eventStore, IKeyStore keyStore)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(eventStore, "eventStore");
            Guard.AgainstNull(keyStore, "keyStore");

            _databaseGateway = databaseGateway;
            _eventStore = eventStore;
            _keyStore = keyStore;
        }

        public ExecutionResult Execute(string formulaName, NameValueCollection arguments)
        {
            var formula = Get<Formula>(formulaName);

            if (formula == null)
            {
                return ExecutionResult.Empty();
            }

            return ExecutionResult.Empty();
        }

        private T Get<T>(string name) where T : class
        {
            var type = typeof(T);
            var keyMethod = type.GetMethod("Key", BindingFlags.Public | BindingFlags.Static);

            var id = _keyStore.Get((string)keyMethod.Invoke(null, new object[] { name }));

            if (!id.HasValue)
            {
                return null;
            }

            var result = Activator.CreateInstance(type, id.Value);

            var stream = _eventStore.Get(id.Value);

            if (stream.IsEmpty)
            {
                return null;
            }

            stream.Apply(result);

            return (T)result;
        }
    }
}