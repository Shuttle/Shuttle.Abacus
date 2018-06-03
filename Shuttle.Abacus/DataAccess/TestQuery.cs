using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class TestQuery : ITestQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly ITestQueryFactory _queryFactory;

        public TestQuery(IDatabaseGateway databaseGateway, ITestQueryFactory queryFactory)
        {
            Guard.AgainstNull(databaseGateway, nameof(databaseGateway));
            Guard.AgainstNull(queryFactory, nameof(queryFactory));

            _databaseGateway = databaseGateway;
            _queryFactory = queryFactory;
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_queryFactory.All());
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_queryFactory.Get(id));
        }

        public IEnumerable<DataRow> ArgumentValues(Guid id)
        {
            return _databaseGateway.GetRowsUsing(_queryFactory.ArgumentValues(id));
        }

        public void Register(Guid id, string name, string formulaName, string expectedResult, string expectedResultType,
            string comparison)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.Register(id, name, formulaName, expectedResult,
                expectedResultType, comparison));
        }

        public void Remove(Guid id)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.RemoveArgumentValues(id));
            _databaseGateway.ExecuteUsing(_queryFactory.Remove(id));
        }

        public void Rename(Guid id, string name)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.Rename(id, name));
        }

        public void SetArgumentValue(Guid id, string argumentName, string value)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.RemoveArgumentValue(id, argumentName));
            _databaseGateway.ExecuteUsing(_queryFactory.AddArgumentValue(id, argumentName, value));
        }
    }
}