using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class MethodQuery : IMethodQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IMethodQueryFactory _methodQueryFactory;

        public MethodQuery(IDatabaseGateway databaseGateway, IMethodQueryFactory methodQueryFactory)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(methodQueryFactory, "methodQueryFactory");

            _databaseGateway = databaseGateway;
            _methodQueryFactory = methodQueryFactory;
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_methodQueryFactory.All());
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_methodQueryFactory.Get(id));
        }
    }
}