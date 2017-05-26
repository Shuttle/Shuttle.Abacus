using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class MatrixRepository : IMatrixRepository
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IMatrixQueryFactory _matrixQueryFactory;

        public MatrixRepository(IDatabaseGateway databaseGateway, IMatrixQueryFactory matrixQueryFactory)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(matrixQueryFactory, "matrixQueryFactory");

            _databaseGateway = databaseGateway;
            _matrixQueryFactory = matrixQueryFactory;
        }

        public void Add(Matrix item)
        {
            _databaseGateway.ExecuteUsing(_matrixQueryFactory.Add(item));
        }

        public void Remove(Guid id)
        {
            _databaseGateway.ExecuteUsing(_matrixQueryFactory.Remove(id));
        }

        public Matrix Get(Guid id)
        {
            var matrixRow = _databaseGateway.GetSingleRowUsing(_matrixQueryFactory.Get(id));

            Guarded.Entity<Matrix>(matrixRow, id);

            var result = new Matrix(
                MatrixColumns.Id.MapFrom(matrixRow),
                MatrixColumns.Name.MapFrom(matrixRow),
                MatrixColumns.RowArgumentName.MapFrom(matrixRow),
                MatrixColumns.ColumnArgumentName.MapFrom(matrixRow));

            return result;
        }

        public void Save(Matrix matrix)
        {
            _databaseGateway.ExecuteUsing(_matrixQueryFactory.Save(matrix));
        }
    }
}