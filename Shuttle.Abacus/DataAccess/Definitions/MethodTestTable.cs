using System;
using Abacus.Domain;

namespace Abacus.Data
{
    public static class MethodTestTable
    {
        public const string ArgumentAnswerTableName = "MethodTestArgumentAnswer";
        public const string TableName = "MethodTest";

        public static IQuery Remove(MethodTest item)
        {
            return DeleteBuilder.Where(MethodTestColumns.Id).EqualTo(item.Id).From(TableName);
        }

        public static IQuery Add(MethodTest item)
        {
            return InsertBuilder.Insert()
                .Add(MethodTestColumns.Id).WithValue(item.Id)
                .Add(MethodTestColumns.MethodId).WithValue(item.MethodId)
                .Add(MethodTestColumns.Description).WithValue(item.Description)
                .Add(MethodTestColumns.ExpectedResult).WithValue(item.ExpectedResult)
                .Into(TableName);
        }

        public static IQuery AddArgumentAnswer(MethodTest test, MethodTestArgumentAnswer argumentAnswer)
        {
            return InsertBuilder
                .Insert()
                .Add(MethodTestColumns.ArgumentAnswerColumns.MethodTestId).WithValue(test.Id)
                .Add(MethodTestColumns.ArgumentAnswerColumns.ArgumentId).WithValue(argumentAnswer.ArgumentId)
                .Add(MethodTestColumns.ArgumentAnswerColumns.ArgumentName).WithValue(argumentAnswer.ArgumentName)
                .Add(MethodTestColumns.ArgumentAnswerColumns.AnswerType).WithValue(argumentAnswer.AnswerType)
                .Add(MethodTestColumns.ArgumentAnswerColumns.Answer).WithValue(argumentAnswer.Answer)
                .Into(ArgumentAnswerTableName);
        }

        public static IQuery Get(Guid id)
        {
            var query = SelectQuery.CreateSelectFrom(@"
                    select
                        mt.MethodTestId,
                        MethodId,
                        Description,
                        ExpectedResult,
                        ArgumentId,
                        ArgumentName,
                        AnswerType,
                        Answer
                    from
                        MethodTest mt
                            inner join
                        MethodTestArgumentAnswer mtaa
                            on (mtaa.MethodTestId = mt.MethodTestId)
                    where
                        mt.MethodTestId = @MethodTestId
                    ");

            query.AddParameterValue(MethodTestColumns.Id, id);

            return query;
        }

        public static IQuery SetArgumentName(Guid argumentId, string argumentName)
        {
            return UpdateBuilder.Update(ArgumentAnswerTableName)
                .Set(MethodTestColumns.ArgumentAnswerColumns.ArgumentName).ToValue(argumentName)
                .Where(MethodTestColumns.ArgumentAnswerColumns.ArgumentId).HasValue(argumentId);
        }

        public static IQuery SetArgumentAnswerType(Guid argumentId, string answerType)
        {
            return UpdateBuilder.Update(ArgumentAnswerTableName)
                .Set(MethodTestColumns.ArgumentAnswerColumns.AnswerType).ToValue(answerType)
                .Where(MethodTestColumns.ArgumentAnswerColumns.ArgumentId).HasValue(argumentId);
        }
    }
}
