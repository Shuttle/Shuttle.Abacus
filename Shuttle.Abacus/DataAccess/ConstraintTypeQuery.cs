using System.Collections.Generic;
using System.Data;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class ConstraintTypeQuery :IConstraintTypeQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IConstraintTypeQueryFactory _constraintTypeQueryFactory;

        public ConstraintTypeQuery(IDatabaseGateway databaseGateway, IConstraintTypeQueryFactory constraintTypeQueryFactory)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(constraintTypeQueryFactory, "constraintTypeQueryFactory");

            _databaseGateway = databaseGateway;
            _constraintTypeQueryFactory = constraintTypeQueryFactory;
        }
        
        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_constraintTypeQueryFactory.All());
        }
    }
}
