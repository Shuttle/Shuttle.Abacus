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
                .AddParameterValue(MatrixColumns.MatrixId, id);
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
                .AddParameterValue(MatrixColumns.MatrixId, id);
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
                .AddParameterValue(MatrixColumns.MatrixId, item.Id)
                .AddParameterValue(MatrixColumns.Name, item.Name)
                .AddParameterValue(MatrixColumns.RowArgumentName, item.RowArgumentName)
                .AddParameterValue(MatrixColumns.ColumnArgumentName, item.ColumnArgumentName);
        }

        public IQuery Remove(Guid id)
        {
            return
                RawQuery.Create("delete from Matrix where MatrixId = @MatrixId")
                    .AddParameterValue(MatrixColumns.MatrixId, id);
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
                .AddParameterValue(MatrixColumns.MatrixId, item.Id);
        }
    }
}