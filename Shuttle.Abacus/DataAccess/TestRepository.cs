using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly ITestQueryFactory _testQueryFactory;
        private readonly IDataTableRepository<Test> _repository;

        public TestRepository(IDatabaseGateway databaseGateway, ITestQueryFactory testQueryFactory,
            IDataTableRepository<Test> repository)
        {
            _repository = repository;
            _databaseGateway = databaseGateway;
            _testQueryFactory = testQueryFactory;
        }

        public override void Add(Test item)
        {
            _databaseGateway.ExecuteUsing(_testQueryFactory.Add(item));

            AddArgumentValue(item);
        }

        public override void Remove(Guid id)
        {
            _databaseGateway.ExecuteUsing(_testQueryFactory.Remove(id));
        }

        public override Test Get(Guid id)
        {
            return _repository.FetchItemUsing(_testQueryFactory.Get(id));
        }

        public void Save(Test item)
        {
            _databaseGateway.ExecuteUsing(_testQueryFactory.Remove(item.Id));
            _databaseGateway.ExecuteUsing(_testQueryFactory.Add(item));

            AddArgumentValue(item);
        }

        private void AddArgumentValue(Test test)
        {
            test.ArgumentValues.ForEach(
                entry => _databaseGateway.ExecuteUsing(_testQueryFactory.AddArgumentAnswer(test, entry)));
        }
    }
}