using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class MethodTestQueryFactory : IMethodTestQueryFactory
    {
        private const string SelectClause = @"
select
    MethodTestId,
    MethodId,
    ExpectedResult,
    Description
from
    MethodTest
";

        public IQuery All()
        {
            return RawQuery.Create(string.Concat(SelectClause, "order by Description"));
        }

        public IQuery AllForMethod(Guid id)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    MethodId = @MethodId
order by 
    Description
"))
                .AddParameterValue(MethodTestColumns.MethodId, id);
        }

        public IQuery GetArgumentAnswers(Guid id)
        {
            return RawQuery.Create(@"
select
    MethodTestId,
    ArgumentId,
    Answer,
    AnswerType,
    ArgumentName
from
    MethodTestArgumentAnswer
where
    MethodTestId = @MethodTestId
order by
    ArgumentName
")
                .AddParameterValue(MethodTestColumns.ArgumentAnswerColumns.MethodTestId, id);
        }

        public IQuery AllUsingArgument(Guid argumentId)
        {
            return RawQuery.Create(@"
select
    Description
from
    MethodTest mt
inner join
    MethodTestArgumentAnswer mtaa on
        (mtaa.MethodTestId = mt.MethodTestId)
where
    mtaa.ArgumentId = @ArgumentId
")
                .AddParameterValue(MethodTestColumns.ArgumentAnswerColumns.ArgumentId, argumentId);
        }

        public IQuery Get(Guid id)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    MethodTestId = @MethodTestId
"))
                .AddParameterValue(MethodTestColumns.Id, id);
        }

        public IQuery Remove(Guid id)
        {
            return
                RawQuery.Create("delete from MethodTest where MethodTestId = @MethodTestId")
                    .AddParameterValue(MethodTestColumns.Id, id);
        }

        public IQuery Add(MethodTest item)
        {
            return RawQuery.Create(@"
insert into MethodTest
(
    MethodTestId,
    MethodId,
    ExpectedResult,
    Description
)
values
(
    @MethodTestId,
    @MethodId,
    @ExpectedResult,
    @Description
)")
                .AddParameterValue(MethodTestColumns.Id, item.Id)
                .AddParameterValue(MethodTestColumns.MethodId, item.MethodId)
                .AddParameterValue(MethodTestColumns.Description, item.Description)
                .AddParameterValue(MethodTestColumns.ExpectedResult, item.ExpectedResult);
        }

        public IQuery AddArgumentAnswer(MethodTest test, MethodTestArgumentAnswer argumentAnswer)
        {
            return RawQuery.Create(@"
insert into MethodTestArgumentAnswer
(
    MethodTestId,
    ArgumentId,
    Answer,
    AnswerType,
    ArgumentName
)
values
(
    @MethodTestId,
    @ArgumentId,
    @Answer,
    @AnswerType,
    @ArgumentName
)")
                .AddParameterValue(MethodTestColumns.ArgumentAnswerColumns.MethodTestId, test.Id)
                .AddParameterValue(MethodTestColumns.ArgumentAnswerColumns.ArgumentId, argumentAnswer.ArgumentId)
                .AddParameterValue(MethodTestColumns.ArgumentAnswerColumns.ArgumentName, argumentAnswer.ArgumentName)
                .AddParameterValue(MethodTestColumns.ArgumentAnswerColumns.AnswerType, argumentAnswer.AnswerType)
                .AddParameterValue(MethodTestColumns.ArgumentAnswerColumns.Answer, argumentAnswer.Answer);
        }

        //public  IQuery Get(Guid id)
        //{
        //    var query = SelectQuery.CreateSelectFrom(@"
        //            select
        //                mt.MethodTestId,
        //                MethodId,
        //                Description,
        //                ExpectedResult,
        //                ArgumentId,
        //                ArgumentName,
        //                AnswerType,
        //                Answer
        //            from
        //                MethodTest mt
        //                    inner join
        //                MethodTestArgumentAnswer mtaa
        //                    on (mtaa.MethodTestId = mt.MethodTestId)
        //            where
        //                mt.MethodTestId = @MethodTestId
        //            ");

        //    query.AddParameterValue(MethodTestColumns.Id, id);

        //    return query;
        //}

        public IQuery SetArgumentName(Guid argumentId, string argumentName)
        {
            return RawQuery.Create(
                    "update MethodTestArgumentAnswer set ArgumentName = @ArgumentName where ArgumentId = @ArgumentId")
                .AddParameterValue(MethodTestColumns.ArgumentAnswerColumns.ArgumentName, argumentName)
                .AddParameterValue(MethodTestColumns.ArgumentAnswerColumns.ArgumentId, argumentId);
        }

        public IQuery SetArgumentAnswerType(Guid argumentId, string answerType)
        {
            return RawQuery.Create(
                    "update MethodTestArgumentAnswer set AnswerType = @AnswerType where ArgumentId = @ArgumentId")
                .AddParameterValue(MethodTestColumns.ArgumentAnswerColumns.AnswerType, answerType)
                .AddParameterValue(MethodTestColumns.ArgumentAnswerColumns.ArgumentId, argumentId);
        }
    }
}