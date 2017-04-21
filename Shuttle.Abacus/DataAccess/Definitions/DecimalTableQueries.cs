using System;

namespace Abacus.Data
{
    public static class DecimalTableQueries
    {
        public const string TableName = "DecimalTable";

        public static ISelectQuery All()
        {
            return SelectBuilder
                .Select(DecimalTableColumns.Id)
                .With(DecimalTableColumns.Name)
                .OrderBy(DecimalTableColumns.Name).Ascending()
                .From(TableName);
        }

        public static ISelectQuery Get(Guid id)
        {
            return SelectBuilder
                .Select(DecimalTableColumns.Id)
                .With(DecimalTableColumns.Name)
                .With(DecimalTableColumns.ColumnArgumentId)
                .With(DecimalTableColumns.RowArgumentId)
                .Where(DecimalTableColumns.Id).EqualTo(id)
                .From(TableName);
        }

        public static IQuery ConstrainedDecimalValues(Guid id)
        {
            var query =
                DynamicQuery.CreateFrom(
                    @"
                select
                    dv.ColumnIndex,
                    dv.RowIndex,
                    dv.DecimalValue,
                    c.Name,
                    c.ArgumentName,
                    c.AnswerType,
                    c.Answer
                from
                    [DecimalValue] dv
                inner join
                    [Constraint] c on
                        (c.OwnerId = dv.DecimalValueId)
                where
                    dv.DecimalTableId = @DecimalTableId");

            query.AddParameterValue(DecimalValueColumns.DecimalTableId, id);

            return query;
        }

        public static ISelectQuery Name(Guid id)
        {
            return SelectBuilder
                .Select(DecimalTableColumns.Name)
                .Where(DecimalTableColumns.Id).EqualTo(id)
                .From(TableName);
        }

        public static IQuery DecimalTableReport(Guid decimalTableId)
        {
            var query =
               DynamicQuery.CreateFrom(
                   @"
                        select 
                            dt.Name As DecimalTableName,
                            ra.Name  As RowArgumentName,
                            ca.Name As ColumnArgumentName,
                            dv.RowIndex,
                            dv.ColumnIndex,
                            dv.DecimalValue,
                            (select MAX(ColumnIndex) from DecimalValue mc where mc.DecimalTableId = dt.DecimalTableId) As MaxColumn,
                            rc.Name As RowConstraint,
                            rc.Answer As RowConstraintTitle,
                            cc.Name As ColumnConstraint,
                            cc.Answer As ColumnConstraintTitle
                        from 
	                        DecimalTable dt
                        inner join 
	                        DecimalValue dv on 
		                        dt.DecimalTableId = dv.DecimalTableId
                        inner join 
	                        Argument ra on 
		                        dt.RowArgumentId = ra.ArgumentId
                        left outer join 
	                        Argument ca on 
		                        dt.ColumnArgumentId = ca.ArgumentId
                        left outer join 
	                        [Constraint] rc on 
		                        dv.DecimalValueId = rc.OwnerId 
		                        and 
		                        rc.SequenceNumber = 1
                        left outer join 
	                        [Constraint] cc on 
		                        dv.DecimalValueId = cc.OwnerId 
		                        and 
		                        cc.SequenceNumber=2
                        where 
                            dt.DecimalTableId = @DecimalTableId
                        order by 
	                        dt.Name, 
	                        dv.RowIndex, 
	                        dv.ColumnIndex
                    ");

            query.AddParameterValue(DecimalTableColumns.Id, decimalTableId);

            return query;
        }
    }
}
