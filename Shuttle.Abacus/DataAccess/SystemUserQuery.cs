using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class SystemUserQuery :ISystemUserQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly ISystemUserQueryFactory _systemUserQueryFactory;

        public SystemUserQuery(IDatabaseGateway databaseGateway,ISystemUserQueryFactory systemUserQueryFactory)
        {
            _databaseGateway = databaseGateway;
            _systemUserQueryFactory = systemUserQueryFactory;
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_systemUserQueryFactory.All());
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_systemUserQueryFactory.Get(id));
        }

        public IEnumerable<DataRow> GetPermissions(Guid id)
        {
            return _databaseGateway.GetRowsUsing(_systemUserQueryFactory.GetPermissions(id));
        }
    }
}
