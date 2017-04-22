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
    Answer
from
    Argument a
left join
    ArgumentRestrictedAnswer ara
        on (a.ArgumentId = ara.ArgumentId) 
";
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

        public IQuery Add(Argument item)
        {
            return RawQuery.Create(@"
insert into Argument
(
    Id,
    Name,
    AnswerType
)
values
(
    @Id,
    @Name,
    @AnswerType
)")
                .AddParameterValue(ArgumentColumns.Id, item.Id)
                .AddParameterValue(ArgumentColumns.Name, item.Name)
                .AddParameterValue(ArgumentColumns.AnswerType, item.AnswerType);
        }

        public IQuery Remove(Argument item)
        {
            return RawQuery.Create("delete from Argument where Id = @Id").AddParameterValue(ArgumentColumns.Id, item.Id);
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


        public IQuery RemoveRestrictedAnswers(Argument argument)
        {
            return RawQuery.Create("delete from ArgumentRestrictedAnswer where ArgumentId = @ArgumentId")
                    .AddParameterValue(ArgumentColumns.RestrictedAnswerColumns.ArgumentId, argument.Id);
        }

        public IQuery SaveRestrictedAnswers(Argument argument, ArgumentRestrictedAnswer argumentRestrictedAnswer)
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
                .AddParameterValue(ArgumentColumns.RestrictedAnswerColumns.ArgumentId, argument.Id)
                .AddParameterValue(ArgumentColumns.RestrictedAnswerColumns.Answer, argumentRestrictedAnswer.Answer);
        }
    }
}