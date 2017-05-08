using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class MatrixRepository : Repository<Matrix>, IMatrixRepository
    {
        private readonly ICache _cache;
        private readonly IDatabaseGateway _databaseGateway;
        private readonly MatrixQueryFactory _matrixQueryFactory;
        private readonly IDataRepository<Matrix> _repository;

        public MatrixRepository(IDatabaseGateway databaseGateway,
            MatrixQueryFactory matrixQueryFactory, IDataRepository<Matrix> repository, ICache cache)
        {
            _repository = repository;
            _databaseGateway = databaseGateway;
            _matrixQueryFactory = matrixQueryFactory;
            _cache = cache;
        }

        public override void Add(Matrix item)
        {
            _databaseGateway.ExecuteUsing(_matrixQueryFactory.Add(item));
        }

        public override void Remove(Guid id)
        {
            _databaseGateway.ExecuteUsing(_matrixQueryFactory.Remove(id));
        }

        public override Matrix Get(Guid id)
        {
            var key = string.Format("Matrix|{0}", id);

            var result = _cache.Get<Matrix>(key);

            if (result != null)
            {
                return result;
            }

            result = _repository.FetchItemUsing(_matrixQueryFactory.Get(id));

            if (result == null)
            {
                throw Exceptions.MissingEntity<DecimalValueQueryFactory>(id);
            }

            _cache.Add(key, result);

            return result;
        }

        public IEnumerable<Matrix> All()
        {
            return _repository.FetchAllUsing(_matrixQueryFactory.All());
        }

        public void Save(Matrix matrix)
        {
            _databaseGateway.ExecuteUsing(_matrixQueryFactory.Save(matrix));
        }
    }
}