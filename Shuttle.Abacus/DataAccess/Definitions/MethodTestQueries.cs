using System;

namespace Abacus.Data
{
    public static class MethodTestQueries
    {
        public const string ArgumentAnswerTableName = "MethodTestArgumentAnswer";
        public const string TableName = "MethodTest";

        public static ISelectQuery All()
        {
            return SelectBuilder
                .Select(MethodTestColumns.Id)
                .With(MethodTestColumns.MethodId)
                .With(MethodTestColumns.Description)
                .With(MethodTestColumns.ExpectedResult)
                .OrderBy(MethodTestColumns.Description).Ascending()
                .From(TableName);
        }

        public static ISelectQuery AllForMethod(Guid id)
        {
            return SelectBuilder
                .Select(MethodTestColumns.Id)
                .With(MethodTestColumns.Description)
                .With(MethodTestColumns.ExpectedResult)
                .Where(MethodTestColumns.MethodId).EqualTo(id)
                .OrderBy(MethodTestColumns.Description).Ascending()
                .From(TableName);
        }

        public static ISelectQuery Get(Guid id)
        {
            return SelectBuilder
                .Select(MethodTestColumns.Id)
                .With(MethodTestColumns.MethodId)
                .With(MethodTestColumns.Description)
                .With(MethodTestColumns.ExpectedResult)
                .Where(MethodTestColumns.Id).EqualTo(id)
                .OrderBy(MethodTestColumns.Description).Ascending()
                .From(TableName);
        }

        public static IQuery Delete(Guid id)
        {
            return DeleteBuilder.Where(MethodTestColumns.Id).EqualTo(id).From(TableName);
        }

        public static ISelectQuery GetArgumentAnswers(Guid id)
        {
            return SelectBuilder
                .Select(MethodTestColumns.ArgumentAnswerColumns.ArgumentId)
                .With(MethodTestColumns.ArgumentAnswerColumns.ArgumentName)
                .With(MethodTestColumns.ArgumentAnswerColumns.AnswerType)
                .With(MethodTestColumns.ArgumentAnswerColumns.Answer)
                .Where(MethodTestColumns.ArgumentAnswerColumns.MethodTestId).EqualTo(id)
                .OrderBy(MethodTestColumns.ArgumentAnswerColumns.ArgumentName).Ascending()
                .From(ArgumentAnswerTableName);
        }

        public static ISelectQuery AllUsingArgument(Guid argumentId)
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
