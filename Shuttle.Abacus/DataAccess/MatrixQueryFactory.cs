using System;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class MatrixQueryFactory : IMatrixQueryFactory
    {
        private const string MatrixQuery = @"
select
    Id,
    Name,
    DataTypeName
from
    Matrix
";

        public IQuery All()
        {
            return RawQuery.Create(string.Concat(MatrixQuery, @"
order by
    Name"));
        }

        public IQuery Get(Guid id)
        {
            return RawQuery.Create(@"
select
    Id,
    Name,
    ColumnArgumentId,
    RowArgumentId,
    DataTypeName
from
    Matrix
")
                .AddParameterValue(Columns.Id, id);
        }

        public IQuery Add(Guid id, string name, Guid? columnArgumentId, Guid rowArgumentId, string dataTypeName)
        {
            return RawQuery.Create(@"
insert into Matrix
(
    Id,
    Name,
    ColumnArgumentId,
    RowArgumentId,
    DataTypeName
)
values
(
    @Id,
    @Name,
    @ColumnArgumentId,
    @RowArgumentId,
    @DataTypeName
)")
                .AddParameterValue(Columns.Id, id)
                .AddParameterValue(Columns.Name, name)
                .AddParameterValue(Columns.ColumnArgumentId, columnArgumentId)
                .AddParameterValue(Columns.RowArgumentId, rowArgumentId)
                .AddParameterValue(Columns.DataTypeName, dataTypeName);
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

        public IQuery ConstraintAdded(Guid matrixId, string axis, int index, Guid id, string comparison, string value)
        {
            return RawQuery.Create(@"
insert into MatrixConstraint
(
    MatrixId,
    Axis,
    [Index],
    Id,
    Comparison,
    Value
)
values
(
    @MatrixId,
    @Axis,
    @Index,
    @Id,
    @Comparison,
    @Value
)
")
                .AddParameterValue(Columns.MatrixId, matrixId)
                .AddParameterValue(Columns.Axis, axis)
                .AddParameterValue(Columns.Index, index)
                .AddParameterValue(Columns.Id, id)
                .AddParameterValue(Columns.Comparison, comparison)
                .AddParameterValue(Columns.Value, value);
        }

        public IQuery ElementAdded(Guid matrixId, int column, int row, Guid id, string value)
        {
            return RawQuery.Create(@"
insert into MatrixElement
(
    MatrixId,
    [Column],
    [Row],
    Id,
    Value
)
values
(
    @MatrixId,
    @Column,
    @Row,
    @Id,
    @Value
)
")
                .AddParameterValue(Columns.MatrixId, matrixId)
                .AddParameterValue(Columns.Column, column)
                .AddParameterValue(Columns.Row, row)
                .AddParameterValue(Columns.Id, id)
                .AddParameterValue(Columns.Value, value);
        }

        public IQuery Search(MatrixSearchSpecification specification)
        {
            Guard.AgainstNull(specification, nameof(specification));

            return new RawQuery(string.Concat(MatrixQuery, @"
where
(
    @Name is null
    or
    @Name = ''
    or
    Name like '%' + @Name + '%'
)
order by 
    Name
"))
                .AddParameterValue(Columns.Name, specification.Name);
        }

        public IQuery Constraints(Guid id)
        {
            return RawQuery.Create(@"
select
    Id,
    MatrixId,
    Axis,
    [Index],
    Comparison,
    Value
from
    MatrixConstraint
where
    MatrixId = @Id
").AddParameterValue(Columns.Id, id);
        }
    }
}