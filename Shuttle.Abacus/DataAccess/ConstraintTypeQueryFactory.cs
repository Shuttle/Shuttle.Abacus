using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class ConstraintTypeQueryFactory : IConstraintTypeQueryFactory
    {
        public IQuery All()
        {
            return RawQuery.Create(@"
select
    ConstraintTypeId,
    Name,
    Text,
    EnabledForRestrictedAnswers
from
    ConstraintType");
        }
    }
}
