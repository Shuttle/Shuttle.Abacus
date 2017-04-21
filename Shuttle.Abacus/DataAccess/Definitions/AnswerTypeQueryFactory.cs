using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public class AnswerTypeQueryFactory : IAnswerTypeQueryFactory
    {
        public IQuery All()
        {
            return RawQuery.Create(@"
select
    Name,
    Text
from
    AnswerType");
        }
    }
}