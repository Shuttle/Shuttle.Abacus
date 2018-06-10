using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class MatrixQueryFactory : IMatrixQueryFactory
    {
        public IQuery All()
        {
            return RawQuery.Create(@"
select
    Id,
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
    Id,
    Name,
    ColumnArgumentName,
    RowArgumentName,
    ValueType
from
    Matrix
")
                .AddParameterValue(Columns.Id, id);
        }

        public IQuery Add(Guid id, string name, string columnArgumentName, string rowArgumentName, string valueType)
        {
            return RawQuery.Create(@"
insert into Matrix
(
    Id,
    Name,
    ColumnArgumentName,
    RowArgumentName,
    ValueType
)
values
(
    @Id,
    @Name,
    @ColumnArgumentName,
    @RowArgumentName,
    @ValueType
)")
                .AddParameterValue(Columns.Id, id)
                .AddParameterValue(Columns.Name, name)
                .AddParameterValue(Columns.ColumnArgumentName, columnArgumentName)
                .AddParameterValue(Columns.RowArgumentName, rowArgumentName)
                .AddParameterValue(Columns.ValueType, valueType);
        }

        public IQuery Remove(Guid id)
        {
            return
                RawQuery.Create("delete from Matrix where Id = @Id")
                    .AddParameterValue(Columns.Id, id);
        }

        public IQuery RemoveElements(Guid id)
        {
            return
                RawQuery.Create("delete from MatrixElement where MatrixId = @Id")
                    .AddParameterValue(Columns.Id, id);
        }

        public IQuery RemoveConstraints(Guid id)
        {
            return
                RawQuery.Create("delete from MatrixConstraint where MatrixId = @Id")
                    .AddParameterValue(Columns.Id, id);
        }

        public IQuery ConstraintAdded(Guid id, int sequenceNumber, string axis, string comparison, string value)
        {
            return RawQuery.Create(@"
insert into MatrixConstraint
(
    MatrixId,
    Axis,
    SequenceNumber,
    Comparison,
    Value
)
values
(
    @Id,
    @Axis,
    @SequenceNumber,
    @Comparison,
    @Value
)
")
                .AddParameterValue(Columns.Id, id)
                .AddParameterValue(Columns.SequenceNumber, sequenceNumber)
                .AddParameterValue(Columns.Axis, axis)
                .AddParameterValue(Columns.Comparison, comparison)
                .AddParameterValue(Columns.Value, value);
        }

        public IQuery ElementAdded(Guid id, int column, int row, string value)
        {
            return RawQuery.Create(@"
insert into MatrixElement
(
    MatrixId,
    [Column],
    [Row],
    Value
)
values
(
    @Id,
    @Column,
    @Row,
    @Value
)
")
                .AddParameterValue(Columns.Id, id)
                .AddParameterValue(Columns.Column, column)
                .AddParameterValue(Columns.Row, row)
                .AddParameterValue(Columns.Value, value);
        }
    }
}