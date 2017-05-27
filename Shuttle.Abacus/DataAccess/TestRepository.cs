using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Infrastructure;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public class TestRepository : ITestRepository
    {
        private readonly IEventStore _eventStore;

        public TestRepository(IEventStore eventStore)
        {
            Guard.AgainstNull(eventStore, "eventStore");

            _eventStore = eventStore;
        }

        public Test Get(Guid id)
        {
            var result = new Test(id);
            var stream = _eventStore.Get(id);

            stream.Apply(result);

            return result;
        }
    }
}