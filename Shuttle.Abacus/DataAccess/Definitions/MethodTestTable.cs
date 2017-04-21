using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public static class MethodTestTable
    {
        public const string ArgumentAnswerTableName = "MethodTestArgumentAnswer";
        

        public static IQuery Remove(MethodTest item)
        {
            return RawQuery.Create("delete from TABLE where Id = @Id").AddParameterValue(MethodTestColumns.Id, item.Id);
        }

        public static IQuery Add(MethodTest item)
        {
            return InsertBuilder.Insert()
                .AddParameterValue(MethodTestColumns.Id, item.Id)
                .AddParameterValue(MethodTestColumns.MethodId, item.MethodId)
                .AddParameterValue(MethodTestColumns.Description, item.Description)
                .AddParameterValue(MethodTestColumns.ExpectedResult, item.ExpectedResult)
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
                .AddParameterValue(MethodTestColumns.ArgumentAnswerColumns.ArgumentId).HasValue(argumentId);
        }

        public static IQuery SetArgumentAnswerType(Guid argumentId, string answerType)
        {
            return UpdateBuilder.Update(ArgumentAnswerTableName)
                .Set(MethodTestColumns.ArgumentAnswerColumns.AnswerType).ToValue(answerType)
                .AddParameterValue(MethodTestColumns.ArgumentAnswerColumns.ArgumentId).HasValue(argumentId);
        }
    }
}
