using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentQueryFactory : IArgumentQueryFactory
    {
        private readonly string SelectClauseDTO = @"
select
    a.ArgumentId,
    Name,
    AnswerType,
    Value
from
    Argument a
left join
    ArgumentValue ara
        on (a.ArgumentId = ara.ArgumentId) 
";
        private readonly string SelectClause = @"
select
    ArgumentId,
    Name,
    AnswerType
from
    Argument
";

        public IQuery All()
        {
            return new RawQuery(string.Concat(SelectClause, "order by Name"));
        }

        public IQuery Get(Guid id)
        {
            return new RawQuery(string.Concat(SelectClause, "where ArgumentId = @ArgumentId")).AddParameterValue(ArgumentColumns.Id, id);
        }

        public IQuery GetValues(Guid id)
        {
            return RawQuery.Create("select Value from ArgumentValue where ArgumentId = @ArgumentId")
                .AddParameterValue(ArgumentColumns.ValueColumns.ArgumentId, id);
        }

        public IQuery Add(Argument item)
        {
            return RawQuery.Create(@"
insert into Argument
(
    ArgumentId,
    Name,
    AnswerType
)
values
(
    @ArgumentId,
    @Name,
    @AnswerType
)")
                .AddParameterValue(ArgumentColumns.Id, item.Id)
                .AddParameterValue(ArgumentColumns.Name, item.Name)
                .AddParameterValue(ArgumentColumns.AnswerType, item.AnswerType);
        }

        public IQuery Remove(Guid id)
        {
            return RawQuery.Create("delete from Argument where ArgumentId = @ArgumentId").AddParameterValue(ArgumentColumns.Id, id);
        }


        public IQuery GetDTO(Guid id)
        {
            return RawQuery.Create(string.Concat(SelectClauseDTO, "where a.ArgumentId = @ArgumentId order by Name"))
                .AddParameterValue(ArgumentColumns.Id, id);
        }

        public IQuery Save(Argument item)
        {
            return RawQuery.Create(@"
update 
    Argument
set
    Name = @Name,
    AnswerType = @AnswerType
where
    ArgumentId = @ArgumentId
")
                .AddParameterValue(ArgumentColumns.Name, item.Name)
                .AddParameterValue(ArgumentColumns.AnswerType, item.AnswerType)
                .AddParameterValue(ArgumentColumns.Id, item.Id);
        }


        public IQuery RemoveValues(Argument argument)
        {
            return RawQuery.Create("delete from ArgumentValue where ArgumentId = @ArgumentId")
                    .AddParameterValue(ArgumentColumns.ValueColumns.ArgumentId, argument.Id);
        }

        public IQuery SaveValue(Argument argument, string value)
        {
            return RawQuery.Create(@"
insert into ArgumentRestrictedAnswer
(
    ArgumentId,
    Answer
)
values
(
    @ArgumentId,
    @Answer
)
")
                .AddParameterValue(ArgumentColumns.ValueColumns.ArgumentId, argument.Id)
                .AddParameterValue(ArgumentColumns.ValueColumns.Value, value);
        }
    }
}