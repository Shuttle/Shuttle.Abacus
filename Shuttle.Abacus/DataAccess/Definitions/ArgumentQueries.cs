using System;

namespace Abacus.Data
{
    public static class ArgumentQueries
    {
        public const string MappingTableName = "ArgumentRestrictedAnswer";
        public const string TableName = "Argument";

        public static ISelectQuery All()
        {
            var query =
                new SelectQuery(
                    @"
                    select
                        ArgumentId,
                        Name,
                        AnswerType,
                        IsSystemData
                    from
                        Argument
                    order by 
                        Name");

            query.AddColumn(ArgumentColumns.Id);
            query.AddColumn(ArgumentColumns.Name);
            query.AddColumn(ArgumentColumns.AnswerType);
            query.AddColumn(ArgumentColumns.IsSystemData);

            return query;
        }

        public static ISelectQuery Get(Guid id)
        {
            return SelectBuilder
                .Select(ArgumentColumns.Id)
                .With(ArgumentColumns.Name)
                .With(ArgumentColumns.AnswerType)
                .With(ArgumentColumns.IsSystemData)
                .Where(ArgumentColumns.Id).EqualTo(id)
                .From(TableName);
        }

        public static ISelectQuery Name(Guid id)
        {
            return SelectBuilder
                .Select(ArgumentColumns.Id)
                .With(ArgumentColumns.Name)
                .Where(ArgumentColumns.Id).EqualTo(id)
                .From(TableName);
        }

        public static ISelectQuery GetRestrictedAnswer(Guid id)
        {
            return SelectBuilder
                .Select(ArgumentColumns.RestrictedAnswerColumns.Answer)
                .Where(ArgumentColumns.RestrictedAnswerColumns.ArgumentId).EqualTo(id)
                .From(MappingTableName);
        }

        public static ISelectQuery Definitions()
        {
            var query =
                SelectQuery.CreateSelectFrom(Select() + " order by Name");


            AddDefinitionColumns(query);

            return query;
        }

        private static void AddDefinitionColumns(ISelectQuery query)
        {
            query.AddColumn(ArgumentColumns.Id);
            query.AddColumn(ArgumentColumns.Name);
            query.AddColumn(ArgumentColumns.AnswerType);
            query.AddColumn(ArgumentColumns.IsSystemData);
            query.AddColumn(ArgumentColumns.RestrictedAnswerColumns.Answer);
        }

        private static string Select()
        {
            return @"
                    select
                        a.ArgumentId,
                        Name,
                        AnswerType,
                        Answer,
                        IsSystemData
                    from
                        Argument a
                    left join
                        ArgumentRestrictedAnswer ara
                            on (a.ArgumentId = ara.ArgumentId)
                    ";
        }

        public static ISelectQuery Definition(Guid argumentId)
        {
            var query =
                SelectQuery.CreateSelectFrom(
                    Select() + "where a.ArgumentId = @ArgumentId");

            query.AddParameterValue(ArgumentColumns.Id, argumentId);

            AddDefinitionColumns(query);

            return query;
        }
    }
}
