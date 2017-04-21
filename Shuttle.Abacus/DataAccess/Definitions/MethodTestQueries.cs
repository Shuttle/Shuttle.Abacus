using System;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public static class MethodTestQueries
    {
        public const string ArgumentAnswerTableName = "MethodTestArgumentAnswer";
        

        public IQuery All()
        {
            return RawQuery.Create(@"
select
                Id,
                MethodId,
                Description,
                ExpectedResult,
                .OrderBy(MethodTestColumns.Description).Ascending()
                .From(TableName);
        }

        public IQuery AllForMethod(Guid id)
        {
            return RawQuery.Create(@"
select
                Id,
                Description,
                ExpectedResult,
                .AddParameterValue(MethodTestColumns.MethodId, id)
                .OrderBy(MethodTestColumns.Description).Ascending()
                .From(TableName);
        }

        public IQuery Get(Guid id)
        {
            return RawQuery.Create(@"
select
                Id,
                MethodId,
                Description,
                ExpectedResult,
                .AddParameterValue(MethodTestColumns.Id, id)
                .OrderBy(MethodTestColumns.Description).Ascending()
                .From(TableName);
        }

        public static IQuery Delete(Guid id)
        {
            return DeleteBuilder.AddParameterValue(MethodTestColumns.Id, id).From(TableName);
        }

        public IQuery GetArgumentAnswers(Guid id)
        {
            return RawQuery.Create(@"
select
                .Select(MethodTestColumns.ArgumentAnswerColumns.ArgumentId)
                .With(MethodTestColumns.ArgumentAnswerColumns.ArgumentName)
                .With(MethodTestColumns.ArgumentAnswerColumns.AnswerType)
                .With(MethodTestColumns.ArgumentAnswerColumns.Answer)
                .AddParameterValue(MethodTestColumns.ArgumentAnswerColumns.MethodTestId, id)
                .OrderBy(MethodTestColumns.ArgumentAnswerColumns.ArgumentName).Ascending()
                .From(ArgumentAnswerTableName);
        }

        public IQuery AllUsingArgument(Guid argumentId)
        {
            var query = SelectQuery.CreateSelectFrom(
                @"
                    select
                        Description
                    from
                        MethodTest mt
                    inner join
                        MethodTestArgumentAnswer mtaa on
                            (tcfa.MethodTestId = mt.MethodTestId)
                    where
                        mtaa.ArgumentId = @ArgumentId
                ");

            query.AddParameterValue(MethodTestColumns.ArgumentAnswerColumns.ArgumentId, argumentId);

            return query;
        }
    }
}
