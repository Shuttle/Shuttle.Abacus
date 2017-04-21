using System;
using Abacus.Domain;

namespace Abacus.Data
{
    public static class ArgumentTableAccess
    {
        public const string RestrictedAnswerTableName = "ArgumentRestrictedAnswer";
        public const string TableName = "Argument";

        public static IQuery Add(Argument item)
        {
            var insert = InsertBuilder.Insert()
                .Add(ArgumentColumns.Id).WithValue(item.Id)
                .Add(ArgumentColumns.Name).WithValue(item.Name)
                .Add(ArgumentColumns.AnswerType).WithValue(item.AnswerType);

            return insert.Into(TableName);
        }

        public static IQuery Remove(Argument item)
        {
            return DeleteBuilder.Where(ArgumentColumns.Id).EqualTo(item.Id).From(TableName);
        }

        public static IQuery Get(Guid id)
        {
            var query = SelectQuery.CreateSelectFrom(ArgumentSql() + @"where a.ArgumentId = @ArgumentId order by Name");

            query.AddParameterValue(ArgumentColumns.Id, id);

            query.AddColumn(ArgumentColumns.Id);
            query.AddColumn(ArgumentColumns.Name);
            query.AddColumn(ArgumentColumns.AnswerType);
            query.AddColumn(ArgumentColumns.RestrictedAnswerColumns.Answer);

            return query;
        }

        private static string ArgumentSql()
        {
            return @"
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
        }

        public static IQuery Save(Argument item)
        {
            return UpdateBuilder.Update(TableName)
                .Set(ArgumentColumns.Name).ToValue(item.Name)
                .Set(ArgumentColumns.AnswerType).ToValue(item.AnswerType)
                .Where(ArgumentColumns.Id).HasValue(item.Id);
        }


        public static IQuery RemoveRestrictedAnswers(Argument argument)
        {
            return
                DeleteBuilder.Where(ArgumentColumns.RestrictedAnswerColumns.ArgumentId).EqualTo(argument.Id).From(
                    RestrictedAnswerTableName);
        }

        public static IQuery SaveRestrictedAnswers(Argument argument, ArgumentRestrictedAnswer argumentRestrictedAnswer)
        {
            return InsertBuilder
                .Insert()
                .Add(ArgumentColumns.RestrictedAnswerColumns.ArgumentId).WithValue(argument.Id)
                .Add(ArgumentColumns.RestrictedAnswerColumns.Answer).WithValue(argumentRestrictedAnswer.Answer)
                .Into(RestrictedAnswerTableName);
        }

        public static IQuery All()
        {
            return SelectQuery.CreateSelectFrom(ArgumentSql() + @"order by Name");
        }
    }
}
