using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class OperationTypeQueryFactory : IOperationTypeQueryFactory
    {
        public IQuery All()
        {
            return RawQuery.Create(@"
select
    Name,
    Text
from
    OperationType
");
        }
    }
}
