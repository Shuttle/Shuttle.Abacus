using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class ValueSourceTypeQueryFactory : IValueSourceTypeQueryFactory
    {
        public IQuery All()
        {
            return RawQuery.Create(@"
select
    Name,
    Text,
    Type
from
    ValueSourceType
order by
    Name");
        }
    }
}
