using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public class DecimalTableQueryFactory
    {
        public const string TableName = "DecimalTable";

        public IQuery All()
        {
            return RawQuery.Create(@"
select
    Id,
    Name
from
    DecimalTable
order by
    Name");
        }

        public IQuery Get(Guid id)
        {
            return RawQuery.Create(@"
select
    Id,
    Name,
    ColumnArgumentId,
    RowArgumentId
from
    DecimalTable
")
                .AddParameterValue(DecimalTableColumns.Id, id);
        }

        public static IQuery ConstrainedDecimalValues(Guid id)
        {
            var query =
                RawQuery.Create(
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

        public IQuery Name(Guid id)
        {
            return RawQuery.Create(@"
select
    Name
from
    DecimalTable
")
                .AddParameterValue(DecimalTableColumns.Id, id);
        }

        public static IQuery DecimalTableReport(Guid decimalTableId)
        {
            var query =
                RawQuery.Create(
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