using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentQuery : IArgumentQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IArgumentQueryFactory _argumentQueryFactory;

        public ArgumentQuery(IDatabaseGateway databaseGateway, IArgumentQueryFactory argumentQueryFactory)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(argumentQueryFactory, "argumentQueryFactory");

            _databaseGateway = databaseGateway;
            _argumentQueryFactory = argumentQueryFactory;
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_argumentQueryFactory.All());
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_argumentQueryFactory.Get(id));
        }

        public IEnumerable<DataRow> GetAnswerCatalog(Guid id)
        {
            return _databaseGateway.GetRowsUsing(_argumentQueryFactory.GetRestrictedAnswer(id));
        }
    }
}