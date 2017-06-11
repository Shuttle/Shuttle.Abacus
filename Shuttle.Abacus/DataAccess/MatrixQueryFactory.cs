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
    ColumnArgumentName,
    RowArgumentName,
    ValueType
from
    Matrix
")
                .AddParameterValue(MatrixColumns.Id, id);
        }

        public IQuery Add(Guid id, string name, string columnArgumentName, string rowArgumentName, string valueType)
        {
            return RawQuery.Create(@"
insert into Matrix
(
    MatrixId,
    Name,
    ColumnArgumentName,
    RowArgumentName,
    ValueType
)
values
(
    @MatrixId,
    @Name,
    @ColumnArgumentName,
    @RowArgumentName,
    @ValueType
)")
                .AddParameterValue(MatrixColumns.Id, id)
                .AddParameterValue(MatrixColumns.Name, name)
                .AddParameterValue(MatrixColumns.ColumnArgumentName, columnArgumentName)
                .AddParameterValue(MatrixColumns.RowArgumentName, rowArgumentName)
                .AddParameterValue(MatrixColumns.ValueType, valueType);
        }

        public IQuery Remove(Guid id)
        {
            return
                RawQuery.Create("delete from Matrix where MatrixId = @MatrixId")
                    .AddParameterValue(MatrixColumns.Id, id);
        }

        public IQuery RemoveElements(Guid id)
        {
            return
                RawQuery.Create("delete from MatrixElement where MatrixId = @MatrixId")
                    .AddParameterValue(MatrixColumns.Id, id);
        }

        public IQuery RemoveConstraints(Guid id)
        {
            return
                RawQuery.Create("delete from MatrixConstraint where MatrixId = @MatrixId")
                    .AddParameterValue(MatrixColumns.Id, id);
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
    @MatrixId,
    @Axis,
    @SequenceNumber,
    @Comparison,
    @Value
)
")
                .AddParameterValue(MatrixColumns.Id, id)
                .AddParameterValue(MatrixColumns.ConstraintColumns.SequenceNumber, sequenceNumber)
                .AddParameterValue(MatrixColumns.ConstraintColumns.Axis, axis)
                .AddParameterValue(MatrixColumns.ConstraintColumns.Comparison, comparison)
                .AddParameterValue(MatrixColumns.ConstraintColumns.Value, value);
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
    @MatrixId,
    @Column,
    @Row,
    @Value
)
")
                .AddParameterValue(MatrixColumns.Id, id)
                .AddParameterValue(MatrixColumns.ElementColumns.Column, column)
                .AddParameterValue(MatrixColumns.ElementColumns.Row, row)
                .AddParameterValue(MatrixColumns.ElementColumns.Value, value);
        }
    }
}