using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public class ArgumentQueryFactory : IArgumentQueryFactory
    {
        private readonly string SelectClause = @"
select
    ArgumentId,
    Name,
    AnswerType,
    IsSystemData
from
    Argument
";

        public IQuery All()
        {
            return new RawQuery(string.Concat(SelectClause, "order by Name"));
        }

        public IQuery Get(Guid id)
        {
            return new RawQuery(string.Concat(SelectClause, "where Id = @Id")).AddParameterValue(ArgumentColumns.Id, id);
        }

        public IQuery GetRestrictedAnswer(Guid id)
        {
            return RawQuery.Create("select Answer from ArgumentRestrictedAnswer where ArgumentId = @ArgumentId")
                    .AddParameterValue(ArgumentColumns.RestrictedAnswerColumns.ArgumentId, id);
        }
    }
}