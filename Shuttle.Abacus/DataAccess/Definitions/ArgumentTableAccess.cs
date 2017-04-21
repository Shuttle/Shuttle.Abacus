using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public static class ArgumentTableAccess
    {
        public const string RestrictedAnswerTableName = "ArgumentRestrictedAnswer";
        

        public static IQuery Add(Argument item)
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

        public static IQuery Remove(Argument item)
        {
            return RawQuery.Create("delete from Argument where Id = @Id").AddParameterValue(ArgumentColumns.Id, item.Id);
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
                .AddParameterValue(ArgumentColumns.Id).HasValue(item.Id);
        }


        public static IQuery RemoveRestrictedAnswers(Argument argument)
        {
            return
                DeleteBuilder.AddParameterValue(ArgumentColumns.RestrictedAnswerColumns.ArgumentId, argument.Id).From(
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
