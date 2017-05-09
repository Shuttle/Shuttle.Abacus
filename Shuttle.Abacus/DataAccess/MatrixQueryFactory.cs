using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class MatrixQueryFactory : IMatrixQueryFactory
    {
        public IQuery All()
        {
            return RawQuery.Create(@"
select
    MatrixId,
    Name
from
    Matrix
order by
    Name");
        }

        public IQuery Get(Guid id)
        {
            return RawQuery.Create(@"
select
    MatrixId,
    Name,
    ColumnArgumentId,
    RowArgumentId
from
    Matrix
")
                .AddParameterValue(MatrixColumns.Id, id);
        }

        public IQuery ConstrainedDecimalValues(Guid id)
        {
            var query =
                RawQuery.Create(
                    @"
                select
                    dv.ColumnIndex,
                    dv.RowIndex,
                    dv.MatrixElement,
                    c.Name,
                    c.ArgumentName,
                    c.AnswerType,
                    c.Answer
                from
                    [MatrixElement] dv
                inner join
                    [Constraint] c on
                        (c.OwnerId = dv.DecimalValueId)
                where
                    dv.MatrixId = @MatrixId");

            query.AddParameterValue(MatrixColumns.ElementColumns.MatrixId, id);

            return query;
        }

        public IQuery Name(Guid id)
        {
            return RawQuery.Create(@"
select
    MatrixId,
    Name
from
    Matrix
")
                .AddParameterValue(MatrixColumns.Id, id);
        }

        public IQuery Report(Guid decimalTableId)
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
                            dv.MatrixElement,
                            (select MAX(ColumnIndex) from MatrixElement mc where mc.MatrixId = dt.MatrixId) As MaxColumn,
                            rc.Name As RowConstraint,
                            rc.Answer As RowConstraintTitle,
                            cc.Name As ColumnConstraint,
                            cc.Answer As ColumnConstraintTitle
                        from 
	                        Matrix dt
                        inner join 
	                        MatrixElement dv on 
		                        dt.MatrixId = dv.MatrixId
                        inner join 
	                        Argument ra on 
		                        dt.RowArgumentId = ra.ArgumentName
                        left outer join 
	                        Argument ca on 
		                        dt.ColumnArgumentId = ca.ArgumentName
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
                            dt.MatrixId = @MatrixId
                        order by 
	                        dt.Name, 
	                        dv.RowIndex, 
	                        dv.ColumnIndex
                    ");

            query.AddParameterValue(MatrixColumns.Id, decimalTableId);

            return query;
        }

        public IQuery Add(Matrix item)
        {
            return RawQuery.Create(@"
insert into Matrix
(
    MatrixId,
    Name,
    RowArgumentName,
    ColumnArgumentName
)
values
(
    @MatrixId,
    @Name,
    @RowArgumentName,
    @ColumnArgumentName
)")
                .AddParameterValue(MatrixColumns.Id, item.Id)
                .AddParameterValue(MatrixColumns.Name, item.Name)
                .AddParameterValue(MatrixColumns.RowArgumentName, item.RowArgumentName)
                .AddParameterValue(MatrixColumns.ColumnArgumentName, item.ColumnArgumentName);
        }

        public IQuery Remove(Guid id)
        {
            return
                RawQuery.Create("delete from Matrix where MatrixId = @MatrixId")
                    .AddParameterValue(MatrixColumns.Id, id);
        }

        public IQuery Save(Matrix item)
        {
            return RawQuery.Create(@"
update Matrix set
    Name = @Name,
    RowArgumentName = @RowArgumentName,
    ColumnArgumentName = @ColumnArgumentName
where
    MatrixId = @MatrixId
")
                .AddParameterValue(MatrixColumns.Name, item.Name)
                .AddParameterValue(MatrixColumns.RowArgumentName, item.RowArgumentName)
                .AddParameterValue(MatrixColumns.ColumnArgumentName, item.ColumnArgumentName)
                .AddParameterValue(MatrixColumns.Id, item.Id);
        }
    }
}